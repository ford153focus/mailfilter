using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Learnings
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host.EndsWith("codewars.com"))
                wMsg.Move("Codewars", new List<string> { "learning", "Codewars" });

            else if (wMsg.Host.EndsWith("coursera.org"))
                wMsg.Move("Coursera", new List<string> { "learning", "Coursera" });

            else if (wMsg.Host.EndsWith("duolingo.com"))
                wMsg.Move("Duolingo", new List<string> { "learning", "Duolingo" });

            else if (wMsg.Host.EndsWith("epam.com"))
                wMsg.Move("learning // EPAM", new List<string> { "learning", "EPAM" });

            else if (wMsg.Host.EndsWith("innopolis.university") || wMsg.SenderAddress == "info@itcenter.expert")
                wMsg.Move("Innopolis", new List<string> { "learning", "Innopolis" });

            else if (wMsg.Host.EndsWith("leadersofdigital.ru"))
                wMsg.Move("Цифровой Прорыв", new List<string> { "learning", "Цифровой Прорыв" });

            else if (wMsg.SenderAddress == "emily.turner@nginx.com" || wMsg.SenderAddress == "e.turner@f5.com")
                wMsg.Move("Learn // Nginx", new List<string> { "learning", "nginx" });

            else if (wMsg.Host == "pluralsight.com")
                wMsg.Move("Learn // Pluralsight", new List<string> { "learning", "Pluralsight" });

            else if (wMsg.SenderAddress == "academy@1c-bitrix.ru")
                wMsg.Move("Learn // 1С-Битрикс Академия", new List<string> { "learning", "1С-Битрикс Академия" });
        }
    }
}
