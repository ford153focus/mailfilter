using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Socials
    {
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
                    wMsg.Move("social // LinkedIn", new List<string> { "social", "LinkedIn" });
                    return;
                case "pixiv.net":
                    wMsg.Move("social // pixiv", new List<string> { "social", "pixiv" });
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

            if (wMsg.Host.EndsWith("github.com"))
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
