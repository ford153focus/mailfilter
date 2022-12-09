using Microsoft.Exchange.WebServices.Data;

namespace ExchangeFilter;

public class WrappedMessage
{
    public EmailMessage origin;

    public WrappedMessage(EmailMessage email)
    {
        origin = email;
    }

    public string From {
        get {
            return origin.From.Address;
        }
    }

    public string Body {
        get {
            string text = origin.Body.Text;
            text = text.Replace("\r", " ").Replace("\n", " "); // remove new line symbols
            text = text.Replace("  ", " ").Replace("  ", " "); // remove extra spaces
            text = text.ToLower().Trim();
            return text;
        }
    }

    public string Recipient {
        get {
            var recipients = origin.ToRecipients;
            return recipients.Count == 0 ? "" : recipients.First().Address;
        }
    }

    public string Subject {
        get {
            if (origin.Subject is null) return "";
            return origin.Subject.ToLower().Trim();
        }
    }
}
