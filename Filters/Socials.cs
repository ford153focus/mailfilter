using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Socials
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.host)
            {
                case "facebookmail.com":
                    Utils.MoveMessage("social // facebook", new List<string> { "social", "facebook" }, wMsg);
                    return;
                case "mail.instagram.com":
                case "instagram.com":
                    Utils.MoveMessage("social // instagram", new List<string> { "social", "instagram" }, wMsg);
                    return;
                case "linkedin.com":
                    Utils.MoveMessage("social // LinkedIn", new List<string> { "social", "LinkedIn" }, wMsg);
                    return;
                case "pixiv.net":
                    Utils.MoveMessage("social // pixiv", new List<string> { "social", "pixiv" }, wMsg);
                    return;
                case "twitter.com":
                    Utils.MoveMessage("social // twitter", new List<string> { "social", "twitter" }, wMsg);
                    return;
                case "vk.com":
                case "notify.vk.com":
                    Utils.MoveMessage("social // vk", new List<string> { "social", "vk" }, wMsg);
                    return;
                case "youtube.com":
                    Utils.MoveMessage("social // youtube", new List<string> { "social", "youtube" }, wMsg);
                    return;
            }

            if (wMsg.host.Contains("pinterest.com"))
            {
                Utils.MoveMessage("social // pinterest", new List<string> { "social", "pinterest" }, wMsg);
                return;
            }
        }
    }
}
