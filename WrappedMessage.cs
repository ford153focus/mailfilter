using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailFilter
{
    public class WrappedMessage
    {
        public ImapClient Client{get;}
        public IMailFolder Inbox{get;}
        public List<string> Filters{get;}

        public int Index{get;}
        public MimeMessage Message{get;}

        public string Host{get;}
        public string SenderAddress{get;}
        public string SenderName{get;}

        public WrappedMessage(ImapClient client, IMailFolder inbox, int index, List<string> filters)
        {
            this.Client = client;
            this.Inbox = inbox;
            this.Index = index;
            this.Filters = filters;

            Message = inbox.GetMessage(index);

            SenderAddress = Message.From.Mailboxes.FirstOrDefault()?.Address;
            SenderName = Message.From.Mailboxes.FirstOrDefault()?.Name;

            MailAddress senderAddressObj = new MailAddress(SenderAddress ?? string.Empty);
            Host = senderAddressObj.Host;
        }

        public void Delete()
        {
            Inbox.AddFlags(Index, MessageFlags.Deleted, true);
            Inbox.Expunge();
            ConsoleUtils.WriteError("deleted");
        }

        public void Move(string outputMessage, List<string> targetFolder)
        {
            try
            {
                // Get subfolder
                // Try to create subfolder on error
                IMailFolder currentFolder = Client.GetFolder(Client.PersonalNamespaces[0]);
                foreach (var folder in targetFolder)
                {
                    try
                    {
                        currentFolder = currentFolder.GetSubfolder(folder);
                    }
                    catch (Exception)
                    {
                        currentFolder = currentFolder.Create(folder, true);
                    }
                }

                // Move
                Inbox.MoveTo(Index, currentFolder);
                Inbox.Expunge();

                ConsoleUtils.WriteSuccess(outputMessage);
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteError(e.ToString());
            }
        }
    }
}
