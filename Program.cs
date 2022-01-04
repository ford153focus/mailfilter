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

namespace MailFilter
{
    internal class Program
    {
        private static void ProcessMailbox(dynamic mailbox)
        {
            ConsoleUtils.WriteWarning(mailbox["login"]);

            // For demo-purposes, accept all SSL certificates
            var client = new ImapClient { ServerCertificateValidationCallback = (s, c, h, e) => true };

            client.Connect(mailbox["host"], mailbox["port"], true);

            client.Authenticate(mailbox["login"], mailbox["password"]);

            // The Inbox folder is always available on all IMAP servers...
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            // convert filters: from JsonArray to List of strings
            var filters = new List<string>();
            foreach (dynamic filter in mailbox["applicable_filters"]) {
                filters.Add(filter.ToString().Trim('"'));
            }

            for (var i = 0; i < inbox.Count; i++)
            {
                var wrappedMessage = new WrappedMessage(client, inbox, i, filters);
                ProcessMessage(wrappedMessage);
            }

            client.Disconnect(true);
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
                Type.GetType("MailFilter.Filters."+filter)
                    .GetMethod("Filter")
                    .Invoke(null, parametersArray);
            }
        }

        private static async Task Main()
        {
            var mailboxesCfgPath = Path.Combine(Environment.CurrentDirectory, "mailboxes.json");
            var mailboxesCfgStr = await File.ReadAllTextAsync(mailboxesCfgPath, Encoding.UTF8);

            JsonValue mailboxesCfg = JsonValue.Parse(mailboxesCfgStr);
            JsonArray mailboxes = (JsonArray)mailboxesCfg["mailboxes"];

            var mailboxTasks = new List<Task>();

            foreach (JsonValue mailbox in mailboxes)
            {
                mailboxTasks.Add(
                    Task.Run(() =>
                    {
                        try
                        {
                            ProcessMailbox(mailbox);
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
