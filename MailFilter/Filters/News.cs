using System.Collections.Generic;

namespace MailFilter.Filters;

internal class News
{
    public static void Filter(WrappedMessage wMsg)
    {
        switch (wMsg.Host)
        {
            case "amd-member.com":
                wMsg.Move("News // AMD", new List<string> { "News", "AMD" });
                return;
            case "digitalocean.com":
            case "info.digitalocean.com":
            case "news.digitalocean.com":
                wMsg.Move("News // DigitalOcean", new List<string> { "News", "DigitalOcean" });
                return;
            case "f1fanvoice.com":
                wMsg.Move("News // Formula1", new List<string> { "News", "Formula1" });
                return;
            case "medium.com":
                wMsg.Move("News :: medium", new List<string> { "News", "medium" });
                return;
            case "microsoft.com":
            case "e-mail.microsoft.com":
            case "email.microsoft.com":
                wMsg.Move("News // Microsoft", new List<string> { "News", "Microsoft" });
                return;
            case "my.motogp.com":
                wMsg.Move("News // Moto GP", new List<string> { "News", "Moto GP" });
                return;
            case "news.ostrovok.ru":
                wMsg.Move("promo // ostrovok", new List<string> { "promo", "ostrovok" });
                return;
            case "update.strava.com":
                wMsg.Move("News // Strava", new List<string> { "News", "Strava" });
                return;
            case "tjournal.ru":
                wMsg.Move("News // Tj", new List<string> { "News", "Tj" });
                return;
            case "announcements.soundcloud.com":
                wMsg.Move("News // SoundCloud", new List<string> { "News", "SoundCloud" });
                return;
            case "qt.io":
                wMsg.Move("News // Qt", new List<string> { "News", "Qt" });
                return;
            case "gorod.io":
                wMsg.Move("News // pol // Горожанин", new List<string> { "News", "pol", "Горожанин" });
                return;
            case "5steps.vote":
            case "navalny.com":
            case "rus.vote":
                wMsg.Move("20!8", new List<string> { "News", "pol", "n2018" });
                return;
        }

        switch (true)
        {
            case true when wMsg.SenderAddress.Equals("subscribe@aviasales.ru"):
                wMsg.Move("promo // aviasales.ru", new List<string> { "promo", "aviasales.ru" });
                return;
            case true when wMsg.Host.EndsWith("change.org"):
                wMsg.Move("News // pol // Change.org", new List<string> { "News", "pol", "Change.org" });
                return;
            case true when wMsg.Host.EndsWith("mozilla.org"):
                wMsg.Move("News // Mozilla", new List<string> { "News", "Mozilla" });
                return;
            case true when wMsg.Host.EndsWith("vc.ru"):
                wMsg.Move("News // vc.ru", new List<string> { "News", "vc.ru" });
                return;
        }
    }
}
