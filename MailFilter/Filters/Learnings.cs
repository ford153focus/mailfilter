using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Learnings
    {
        public static void Filter(WrappedMessage wMsg)
        {
            Coursera(wMsg);

            if (wMsg.Host.EndsWith("codewars.com"))
                wMsg.Move("Codewars", new List<string> { "learning", "Codewars" });

            else if (wMsg.Host.EndsWith("duolingo.com"))
                wMsg.Move("Duolingo", new List<string> { "learning", "Duolingo" });

            else if (wMsg.Host.EndsWith("epam.com"))
                wMsg.Move("learning // EPAM", new List<string> { "learning", "EPAM" });

            else if (wMsg.Host.EndsWith("innopolis.university") || wMsg.Host.EndsWith("innopolis.ru") || wMsg.SenderAddress == "info@itcenter.expert")
                wMsg.Move("Innopolis", new List<string> { "learning", "Innopolis" });

            else if (wMsg.Host.EndsWith("leadersofdigital.ru"))
                wMsg.Move("Цифровой Прорыв", new List<string> { "learning", "Цифровой Прорыв" });

            else if (wMsg.Host.EndsWith("nginx.com") || wMsg.Host.EndsWith("f5.com"))
                wMsg.Move("Learn // Nginx", new List<string> { "learning", "nginx" });

            else if (wMsg.Host == "pluralsight.com")
                wMsg.Move("Learn // Pluralsight", new List<string> { "learning", "Pluralsight" });

            else if (wMsg.SenderAddress == "academy@1c-bitrix.ru")
                wMsg.Move("Learn // 1С-Битрикс Академия", new List<string> { "learning", "1С-Битрикс Академия" });
        }

        public static void Coursera(WrappedMessage wMsg)
        {
            if (!wMsg.Host.EndsWith("coursera.org")) return;

            if (wMsg.Message.Subject == "Популярное на Coursera на этой неделе")
            {
                wMsg.Move("Coursera :: Popular", new List<string> { "learning", "Coursera", "popular" });
            }
            else if (wMsg.Message.Subject == "Рекомендуемые курсы" || wMsg.Message.Subject.StartsWith("Рекомендации:"))
            {
                wMsg.Move("Coursera :: Recommended", new List<string> { "learning", "Coursera", "recommended" });
            }
            else
            {
                wMsg.Move("Coursera", new List<string> { "learning", "Coursera" });
            }
        }
    }
}
