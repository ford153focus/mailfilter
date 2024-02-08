using System.Collections.Generic;

namespace MailFilter.Filters;

internal class Freelance
{
    public static void Filter(WrappedMessage wMsg)
    {
        if (wMsg.Host == "robot.freelance.ru")
        {
            wMsg.Move("Freelance // Freelance.Ru", new List<string> { "Freelance", "Freelance.Ru" });
            return;
        }

        if (wMsg.Host == "free-lance.ru" || wMsg.Host.Contains("fl.ru"))
        {
            wMsg.Move("Freelance // FL.ru", new List<string> { "Freelance", "FL.ru" });
            return;
        }

        if (wMsg.Host == "headz.io")
        {
            wMsg.Move("Freelance // headz.io", new List<string> { "Freelance", "headz.io" });
            return;
        }

        if (wMsg.Host.Contains("upwork.com"))
        {
            wMsg.Move("Freelance // Upwork", new List<string> { "Freelance", "Upwork" });
            return;
        }
    }
}