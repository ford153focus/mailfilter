using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailFilter
{
    internal class Program
    {
        private static async void ProcessMailboxAsync(dynamic mailbox)
        {
            Utils.WarningWrite(mailbox["login"]);

            // For demo-purposes, accept all SSL certificates
            var client = new ImapClient { ServerCertificateValidationCallback = (s, c, h, e) => true };

            client.Connect(mailbox["host"], mailbox["port"], true);

            client.Authenticate(mailbox["login"], mailbox["password"]);

            // The Inbox folder is always available on all IMAP servers...
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            for (var i = 0; i < inbox.Count; i++)
            {
                // convert JsonArray to List
                var filters = new List<string>();
                foreach (dynamic filter in mailbox["applicable_filters"]) filters.Add(filter);
                ProcessMessage(client, inbox, i, filters);
            }

            client.Disconnect(true);
        }

        private static void ProcessMessage(ImapClient client, IMailFolder inbox, int i, List<string> filters)
        {
            MimeMessage message = inbox.GetMessage(i);

            Console.WriteLine(
                "Subject: '{0}' from '{1}' to '{2}'",
                message.Subject,
                !message.From.Mailboxes.Any() ? null : message.From.Mailboxes.First(),
                !message.To.Mailboxes.Any() ? null : message.To.Mailboxes.First()
            );

            foreach (var filter in filters)
            {
                object[] parametersArray = { client, inbox, message, i };
                MethodInfo method = typeof(Filters).GetMethod(filter.Trim('"'));
                method.Invoke(null, parametersArray);
            }
        }

        private static async Task Main()
        {
            var mailboxesCfgPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "mailboxes.json";
            var mailboxesCfgStr = File.ReadAllText(mailboxesCfgPath, Encoding.UTF8);
            JsonValue mailboxesCfg = JsonValue.Parse(mailboxesCfgStr);

            var mailboxTasks = new List<Task>();

            foreach (object mailbox in mailboxesCfg["mailboxes"])
            {
                mailboxTasks.Add(
                    Task.Run(() => { ProcessMailboxAsync(mailbox); })
                );
            }

            await Task.WhenAll(mailboxTasks);
        }
    }
}