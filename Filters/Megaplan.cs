using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Megaplan
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.SenderAddress)
            {
                case "weekly@megaplan.ru":
                    wMsg.Move("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" });
                    break;
            }

            switch (wMsg.SenderName)
            {
                case "Мегаплан. Полезное чтение":
                case "Полезное чтение":
                    wMsg.Move("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" });
                    break;
            }
        }
    }
}
