using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Megaplan
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.senderAddress)
            {
                case "weekly@megaplan.ru":
                    Utils.MoveMessage("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" }, wMsg);
                    break;
            }

            switch (wMsg.senderName)
            {
                case "Мегаплан. Полезное чтение":
                    Utils.MoveMessage("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" }, wMsg);
                    break;
            }
        }
    }
}
