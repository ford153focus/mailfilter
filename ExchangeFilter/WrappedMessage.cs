using Microsoft.Exchange.WebServices.Data;

namespace ExchangeFilter;

public class WrappedMessage
{
    public readonly EmailMessage Origin;

    public WrappedMessage(EmailMessage email)
    {
        Origin = email;
    }

    public string From => Origin.From.Address;

    public string Body {
        get {
            string text = Origin.Body.Text;
            text = text.Replace("\r", " ").Replace("\n", " "); // remove new line symbols
            text = text.Replace("  ", " ").Replace("  ", " "); // remove extra spaces
            text = text.ToLower().Trim();
            return text;
        }
    }

    public string Recipient {
        get {
            var recipients = Origin.ToRecipients;
            return recipients.Count == 0 ? "" : recipients.First().Address;
        }
    }

    public string Subject {
        get {
            if (Origin.Subject is null) return "";
            return Origin.Subject.ToLower().Trim();
        }
    }
}
