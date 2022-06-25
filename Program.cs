using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

namespace MailFilter
{
    internal class Program
    {
        private static async Task<ImapClient> GetGmailClient(Settings.Mailbox.Model.Mailbox mailbox)
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = Settings.OAuth.ClientId,
                ClientSecret = Settings.OAuth.ClientSecret
            };

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            // var codeReceiver = new LocalServerCodeReceiver();
            var codeReceiver = new PromptCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);
            var credential = await authCode.AuthorizeAsync(mailbox.login, CancellationToken.None);

            if (credential.Token.IsExpired(SystemClock.Default))
                await credential.RefreshTokenAsync(CancellationToken.None);

            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);

            var client = new ImapClient();
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(oauth2);

            return client;
        }

        private static async Task ProcessMailboxAsync(Settings.Mailbox.Model.Mailbox mailbox)
        {
            ConsoleUtils.WriteWarning(mailbox.login);

            ImapClient client;

            if (mailbox.login.EndsWith("@gmail.com"))
            {
                client = await GetGmailClient(mailbox);
            }
            else
            {
                // For demo-purposes, accept all SSL certificates
                client = new ImapClient { ServerCertificateValidationCallback = (s, c, h, e) => true };
                await client.ConnectAsync(mailbox.host, mailbox.port, true);
                await client.AuthenticateAsync(mailbox.login, mailbox.password);
            }

            // The Inbox folder is always available on all IMAP servers...
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            // convert filters: from JsonArray to List of strings
            var filters = new List<string>();
            foreach (string filter in mailbox.applicable_filters)
            {
                filters.Add(filter.ToString().Trim('"'));
            }

            for (var i = 0; i < inbox.Count; i++)
            {
                var wrappedMessage = new WrappedMessage(client, inbox, i, filters);
                ProcessMessage(wrappedMessage);
            }

            await client.DisconnectAsync(true);
        }

        private static void ProcessMessage(WrappedMessage wMsg)
        {
            Console.WriteLine(
                "Subject: '{0}' from '{1}' to '{2}'",
                wMsg.Message.Subject,
                !wMsg.Message.From.Mailboxes.Any() ? null : wMsg.Message.From.Mailboxes.First(),
                !wMsg.Message.To.Mailboxes.Any() ? null : wMsg.Message.To.Mailboxes.First()
            );

            foreach (var filter in wMsg.Filters)
            {
                object[] parametersArray = { wMsg };
                Type.GetType("MailFilter.Filters." + filter)
                    .GetMethod("Filter")
                    .Invoke(null, parametersArray);
            }
        }

        private static async Task Main()
        {
            var mailboxTasks = new List<Task>();

            foreach (var mailbox in Settings.Mailbox.GetAll())
            {
                mailboxTasks.Add(
                Task.Run(async () =>
                {
                    try
                    {
                        await ProcessMailboxAsync(mailbox);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                })
                );
            }

            await Task.WhenAll(mailboxTasks);
            ConsoleUtils.WriteSuccess("All mailboxes filtered");
        }
    }
}
