using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class News
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.Host)
            {
                case "amd-member.com":
                    Utils.MoveMessage("News // AMD", new List<string> { "News", "AMD" }, wMsg);
                    return;
                case "digitalocean.com":
                case "info.digitalocean.com":
                case "news.digitalocean.com":
                    Utils.MoveMessage("News // DigitalOcean", new List<string> { "News", "DigitalOcean" }, wMsg);
                    return;
                case "f1fanvoice.com":
                    Utils.MoveMessage("News // Formula1", new List<string> { "News", "Formula1" }, wMsg);
                    return;
                case "microsoft.com":
                case "e-mail.microsoft.com":
                case "email.microsoft.com":
                    Utils.MoveMessage("News // Microsoft", new List<string> { "News", "Microsoft" }, wMsg);
                    return;
                    case "my.motogp.com":
                    Utils.MoveMessage("News // Moto GP", new List<string> { "News", "Moto GP" }, wMsg);
                    return;
                case "mozilla.org":
                case "e.mozilla.org":
                    Utils.MoveMessage("News // Mozilla", new List<string> { "News", "Mozilla" }, wMsg);
                    return;
                case "5steps.vote":
                case "navalny.com":
                    Utils.MoveMessage("20!8", new List<string> { "News", "n2018" }, wMsg);
                    return;
                case "tjournal.ru":
                    Utils.MoveMessage("News // Tj", new List<string> { "News", "Tj" }, wMsg);
                    return;
                case "qt.io":
                    Utils.MoveMessage("News // Qt", new List<string> { "News", "Qt" }, wMsg);
                    return;
                case "vc.ru":
                    Utils.MoveMessage("News // vc.ru", new List<string> { "News", "vc.ru" }, wMsg);
                    return;
            }
        }
    }
}
