using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Yandex
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.SenderAddress)
            {
                case "cloud@support.yandex.ru":
                case "CloudPartnerHelp@yandex.ru":
                    Utils.MoveMessage("Yandex // Cloud", new List<string> { "Yandex", "Cloud" }, wMsg);
                    break;
                case "events@support.yandex.ru":
                    Utils.MoveMessage("Yandex // Events", new List<string> { "Yandex", "Events" }, wMsg);
                    break;
                case "noreply@eda.yandex.ru":
                    Utils.MoveMessage("Yandex // Eda", new List<string> { "Yandex", "Eda" }, wMsg);
                    break;
            }

            switch (wMsg.SenderName)
            {
                case "Yandex.Webmaster":
                case "Яндекс.Вебмастер":
                    Utils.MoveMessage("Yandex // Webmaster", new List<string> { "Yandex", "Webmaster" }, wMsg);
                    break;
                case "Yandex.Connect":
                case "Яндекс.Коннект":
                    Utils.MoveMessage("Yandex // Connect", new List<string> { "Yandex", "Connect" }, wMsg);
                    break;
                case "Яндекс.Трекер":
                    Utils.MoveMessage("Yandex // Tracker", new List<string> { "Yandex", "Tracker" }, wMsg);
                    break;
            }

            switch (wMsg.Host)
            {
                case "chef.yandex.ru":
                    Utils.MoveMessage("Yandex // Chef", new List<string> { "Yandex", "Chef" }, wMsg);
                    break;
                case "cloud.yandex.ru":
                    Utils.MoveMessage("Yandex // Cloud", new List<string> { "Yandex", "Cloud" }, wMsg);
                    break;
                case "disk.yandex.ru":
                    Utils.MoveMessage("Yandex // Disk", new List<string> { "Yandex", "Disk" }, wMsg);
                    break;
                case "kinopoisk.ru":
                    Utils.MoveMessage("Yandex // Maps", new List<string> { "Yandex", "Maps" }, wMsg);
                    break;
                case "maps.yandex.ru":
                    Utils.MoveMessage("Yandex // КиноПоиск", new List<string> { "Yandex", "КиноПоиск" }, wMsg);
                    break;
                case "market.yandex.ru":
                    Utils.MoveMessage("Yandex // Market", new List<string> { "Yandex", "Market" }, wMsg);
                    break;
                case "money.yandex.ru":
                case "yamoney.ru":
                    Utils.MoveMessage("Yandex // Money", new List<string> { "Yandex", "Money" }, wMsg);
                    break;
                case "realty.yandex.ru":
                    Utils.MoveMessage("Yandex // Realty", new List<string> { "Yandex", "Realty" }, wMsg);
                    break;
            }
        }
    }
}
