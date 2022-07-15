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
        public ImapClient Client { get; }
        public IMailFolder Inbox { get; }
        public List<string> Filters { get; }

        public int Index { get; }
        public MimeMessage Message { get; }
        private string _host;
        public string Host
        {
            get
            {
                if (_host is null)
                {
                    MailAddress senderAddressObj = new MailAddress(SenderAddress ?? string.Empty);
                    _host = senderAddressObj.Host;
                }
                return _host;
            }
        }
        private string _senderAddress;
        public string SenderAddress
        {
            get
            {
                if (_senderAddress is null)
                {
                    _senderAddress = Message.From.Mailboxes.FirstOrDefault()?.Address;
                }
                return _senderAddress;
            }
        }
        private string _senderName;
        public string SenderName
        {
            get
            {
                if (_senderName is null)
                {
                    _senderName = Message.From.Mailboxes.FirstOrDefault()?.Name;
                }
                return _senderName;
            }
        }
        private string _subject;
        public string Subject
        {
            get
            {
                if (_subject is null)
                {
                    _subject = Message.Subject;
                    _subject = _subject.Replace(' ', ' '); // replace nbsp with space
                    _subject = _subject.Replace("  ", " ");
                    _subject = _subject.Replace("  ", " ");
                }
                return _subject;
            }
        }

        public WrappedMessage(ImapClient client, IMailFolder inbox, int index, List<string> filters)
        {
            this.Client = client;
            this.Inbox = inbox;
            this.Index = index;
            this.Filters = filters;

            Message = inbox.GetMessage(index);
        }

        public void Delete()
        {
            try
            {
                Inbox.AddFlags(Index, MessageFlags.Deleted, true);
                Inbox.Expunge();
                ConsoleUtils.WriteError("deleted");
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteError(e.ToString());
            }
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
