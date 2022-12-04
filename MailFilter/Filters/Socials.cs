using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MailFilter.Filters
{
    internal class Socials
    {
        public static void LinkedIn(WrappedMessage wMsg)
        {
            switch (true)
            {
                case true when wMsg.Message.Subject.EndsWith("I’d like to connect"):
                case true when Regex.Match(wMsg.Message.Subject, @"^.+, add .+ to your network$").Success:
                    wMsg.Move("social // LinkedIn // add me", new List<string> { "social", "LinkedIn", "add me" });
                    break;
                case true when Regex.Match(wMsg.Message.Subject, @"^.+ is hiring: .+\.$").Success:
                    wMsg.Move("social // LinkedIn // company X searching new employee", new List<string> { "social", "LinkedIn", "company is hiring" });
                    break;
                case true when wMsg.Message.Subject.Contains("отправьте сообщение своему новому контакту"):
                    wMsg.Move("social // LinkedIn // contact added", new List<string> { "social", "LinkedIn", "contact added" });
                    break;
                case true when wMsg.Message.Subject.Contains("sent you message"):
                case true when wMsg.Message.Subject.EndsWith("just messaged you"):
                case true when Regex.Match(wMsg.Message.Subject, @"^You have \d+ new message(s?)$").Success:
                    wMsg.Move("social // LinkedIn // new message", new List<string> { "social", "LinkedIn", "new message" });
                    break;
                case true when Regex.Match(wMsg.Message.Subject, @"^You appeared in \d+ searche(s?) this week$").Success:
                    wMsg.Move("social // LinkedIn // profile appeared in search", new List<string> { "social", "LinkedIn", "profile appeared in search" });
                    break;
                case true when Regex.Match(wMsg.Message.Subject, @"^\d+ people noticed you$").Success:
                    wMsg.Move("social // LinkedIn // profile attracts attention", new List<string> { "social", "LinkedIn", "profile attracts attention" });
                    break;
                case true when Regex.Match(wMsg.Message.Subject, @"^У участника .+ \d+ новых").Success: //У участника Abc Def 4 новых...
                    wMsg.Delete();
                    break;
                default:
                    // wMsg.Move("social // LinkedIn", new List<string> { "social", "LinkedIn" });
                    break;
            }
        }

        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.Host)
            {
                case "facebookmail.com":
                    wMsg.Move("social // facebook", new List<string> { "social", "facebook" });
                    return;
                case "mail.instagram.com":
                case "instagram.com":
                    wMsg.Move("social // instagram", new List<string> { "social", "instagram" });
                    return;
                case "linkedin.com":
                    LinkedIn(wMsg);
                    return;
                case "pixiv.net":
                    wMsg.Move("social // pixiv", new List<string> { "social", "pixiv" });
                    return;
                case "odnoklassniki.ru":
                    wMsg.Move("social // odnoklassniki", new List<string> { "social", "odnoklassniki" });
                    return;
                case "twitter.com":
                    wMsg.Move("social // twitter", new List<string> { "social", "twitter" });
                    return;
                case "vk.com":
                case "notify.vk.com":
                    wMsg.Move("social // vk", new List<string> { "social", "vk" });
                    return;
                case "youtube.com":
                    wMsg.Move("social // youtube", new List<string> { "social", "youtube" });
                    return;
            }

            if (wMsg.Host.EndsWith("107klub.com"))
            {
                wMsg.Move("social // 107klub.com", new List<string> { "social", "107klub" });
            }
            else if (wMsg.Host.EndsWith("github.com"))
            {
                wMsg.Move("social // GitHub", new List<string> { "social", "GitHub" });
            }
            else if (wMsg.Host.EndsWith("joyreactor.cc"))
            {
                wMsg.Move("social // Joyreactor", new List<string> { "social", "Joyreactor" });
            }
            else if (wMsg.Host.EndsWith("habr.com") && wMsg.Message.Subject.StartsWith("Ответ на ваш комментарий к публикации"))
            {
                wMsg.Move("social // Habr", new List<string> { "social", "Habr" });
            }
            else if (wMsg.Host.EndsWith("pinterest.com"))
            {
                wMsg.Move("social // pinterest", new List<string> { "social", "pinterest" });
            }
            else if (wMsg.Host.EndsWith("redditmail.com"))
            {
                wMsg.Move("social // Reddit", new List<string> { "social", "Reddit" });
            }
            else if (wMsg.Host.EndsWith("stackoverflow.email"))
            {
                wMsg.Move("social // Stack Overflow", new List<string> { "social", "Stack Overflow" });
            }
        }
    }
}
