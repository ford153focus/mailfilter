using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Freelance
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.host == "robot.freelance.ru")
            {
                Utils.MoveMessage("Freelance // Freelance.Ru", new List<string> { "Freelance", "Freelance.Ru" }, wMsg);
                return;
            }

            if (wMsg.host == "free-lance.ru" || wMsg.host.Contains("fl.ru"))
            {
                Utils.MoveMessage("Freelance // FL.ru", new List<string> { "Freelance", "FL.ru" }, wMsg);
                return;
            }

            if (wMsg.host == "headz.io")
            {
                Utils.MoveMessage("Freelance // headz.io", new List<string> { "Freelance", "headz.io" }, wMsg);
                return;
            }

            if (wMsg.host.Contains("upwork.com"))
            {
                Utils.MoveMessage("Freelance // Upwork", new List<string> { "Freelance", "Upwork" }, wMsg);
                return;
            }
        }
    }
}
