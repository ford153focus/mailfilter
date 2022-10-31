using Microsoft.Exchange.WebServices.Data;

namespace ExchangeFilter;

public static class MessageUtils
{
    public static string GetBody(EmailMessage email)
    {
        return email.Body.Text
                    .Replace("\r", " ").Replace("\n", " ") // remove new line symbols
                    .Replace("  ", " ").Replace("  ", " ") // remove extra spaces
                    .ToLower()
                    .Trim();
    }

    public static string GetSubject(EmailMessage email)
    {
        return email.Subject
                    .ToLower()
                    .Trim();
    }
}
