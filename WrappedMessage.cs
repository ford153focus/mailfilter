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
        public ImapClient Client;
        public IMailFolder Inbox;
        public List<string> Filters;

        public int Index;
        public MimeMessage Message;

        public string Host;
        public string SenderAddress;
        public string SenderName;

        public WrappedMessage(ImapClient client, IMailFolder inbox, int index, List<string> filters)
        {
            this.Client = client;
            this.Inbox = inbox;
            this.Index = index;
            this.Filters = filters;

            Message = inbox.GetMessage(index);

            SenderAddress = Message.From.Mailboxes.FirstOrDefault().Address;
            SenderName = Message.From.Mailboxes.FirstOrDefault().Name;

            MailAddress senderAddressObj = new MailAddress(SenderAddress);
            Host = senderAddressObj.Host;
        }
    }
}
