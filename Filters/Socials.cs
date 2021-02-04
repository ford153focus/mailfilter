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

            if (wMsg.Host.Contains("pinterest.com"))
            {
                wMsg.Move("social // pinterest", new List<string> { "social", "pinterest" });
                return;
            }
        }
    }
}
