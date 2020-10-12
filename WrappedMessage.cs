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
        public ImapClient client;
        public IMailFolder inbox;
        public List<string> filters;

        public int index;
        public MimeMessage message;

        public string host;
        public string senderAddress;
        public string senderName;

        public WrappedMessage(ImapClient client, IMailFolder inbox, int index, List<string> filters)
        {
            this.client = client;
            this.inbox = inbox;
            this.index = index;
            this.filters = filters;

            message = inbox.GetMessage(index);

            senderAddress = message.From.Mailboxes.FirstOrDefault().Address;
            senderName = message.From.Mailboxes.FirstOrDefault().Name;

            MailAddress senderAddressObj = new MailAddress(senderAddress);
            host = senderAddressObj.Host;
        }
    }
}
