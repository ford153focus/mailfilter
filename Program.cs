using System;
using System.Collections;
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

namespace Mailfilter {
    class Program {
        public static async void ProccessMailboxAsync (dynamic mailbox) {
            Utils.WarningWrite (mailbox["login"]);

            var client = new ImapClient ();

            // For demo-purposes, accept all SSL certificates
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect (mailbox["host"], mailbox["port"], true);

            client.Authenticate (mailbox["login"], mailbox["password"]);

            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open (FolderAccess.ReadWrite);

            // var messageTasks = new List<Task> ();

            for (int i = 0; i < inbox.Count; i++) {
                // messageTasks.Add (
                    // Task.Run (
                        // () => {
                            /// convert JsonArray to List
                            var filters = new List<String> ();
                            foreach(var filter in mailbox["applicable_filters"]) { 
                                filters.Add(filter);
                            }
                            ProccessMessageAsync (client, inbox, i, filters);
                        // }
                    // )
                // );
            }

            /// if we have running tasks - wait them
            // if (messageTasks.Count > 0) {
                // await Task.WhenAll (messageTasks);
            // }

            client.Disconnect (true);
        }

        public static async void ProccessMessageAsync (ImapClient client, IMailFolder inbox, int i, List<String> filters) {
            var message = inbox.GetMessage (i);

            Console.WriteLine (
                "Subject: '{0}' from '{1}' to '{2}'",
                message.Subject,
                message.From.Mailboxes.Count () == 0 ? null : message.From.Mailboxes.First (),
                message.To.Mailboxes.Count () == 0 ? null : message.To.Mailboxes.First ()
            );

            // var filterTasks = new List<Task> ();

            foreach (var filter in filters) {
                // filterTasks.Add (
                    // Task.Run (
                        // () => {
                            object[] parametersArray = new object[] { client, inbox, message, i };
                            MethodInfo method = typeof (Filters).GetMethod (filter.ToString ().Trim ('"'));
                            method.Invoke (null, parametersArray);
                        // }
                    // )
                // );
            }

            // await Task.WhenAll (filterTasks);
        }

        static async Task Main (string[] args) {
            string mailboxesCfgPath = Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location) + Path.DirectorySeparatorChar + "mailboxes.json";
            string mailboxesCfgStr = File.ReadAllText (mailboxesCfgPath, Encoding.UTF8);
            var mailboxesCfg = JsonValue.Parse (mailboxesCfgStr);

            var mailboxTasks = new List<Task> ();

            foreach (var mailbox in mailboxesCfg["mailboxes"]) {
                mailboxTasks.Add (
                    Task.Run (() => {
                        ProccessMailboxAsync (mailbox);
                    })
                );
            }

            await Task.WhenAll (mailboxTasks);
        }
    }
}
