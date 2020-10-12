using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Learnings
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host.Contains("codewars.com"))
                Utils.MoveMessage("Codewars", new List<string> { "learning", "Codewars" }, wMsg);

            else if (wMsg.Host.Contains("coursera.org"))
                Utils.MoveMessage("Coursera", new List<string> { "learning", "Coursera" }, wMsg);

            else if (wMsg.Host.Contains("duolingo.com"))
                Utils.MoveMessage("Duolingo", new List<string> { "learning", "Duolingo" }, wMsg);

            else if (wMsg.SenderAddress == "events_ru@epam.com")
                Utils.MoveMessage("learning // EPAM", new List<string> { "learning", "EPAM" }, wMsg);

            else if (wMsg.Host.Contains("innopolis.university") || wMsg.SenderAddress == "info@itcenter.expert")
                Utils.MoveMessage("Innopolis", new List<string> { "learning", "Innopolis" }, wMsg);

            else if (wMsg.Host.Contains("leadersofdigital.ru"))
                Utils.MoveMessage("Цифровой Прорыв", new List<string> { "learning", "Цифровой Прорыв" }, wMsg);

            else if (wMsg.SenderAddress == "emily.turner@nginx.com" || wMsg.SenderAddress == "e.turner@f5.com")
                Utils.MoveMessage("Learn // Nginx", new List<string> { "learning", "nginx" }, wMsg);

            else if (wMsg.Host == "pluralsight.com")
                Utils.MoveMessage("Learn // Pluralsight", new List<string> { "learning", "Pluralsight" }, wMsg);

            else if (wMsg.SenderAddress == "academy@1c-bitrix.ru")
                Utils.MoveMessage("Learn // 1С-Битрикс Академия", new List<string> { "learning", "1С-Битрикс Академия" }, wMsg);
        }
    }
}
