using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Stores
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.Host)
            {
                case "05.ru":
                    Utils.MoveMessage("store // 05.ru", new List<string> { "stores", "05.ru" }, wMsg);
                    return;
                case "2bit.ru":
                    Utils.MoveMessage("store // 2bit.ru", new List<string> { "stores", "2bit.ru" }, wMsg);
                    return;
                case "5ka.ru":
                case "mail.5ka.ru":
                    Utils.MoveMessage("store // 5", new List<string> { "stores", "5ka.ru" }, wMsg);
                    return;
                case "9814555.ru":
                    Utils.MoveMessage("store // Мопедофф", new List<string> { "stores", "Мопедофф" }, wMsg);
                    return;
                case "pobeda.aero":
                    Utils.MoveMessage("store // Авиакомпания Победа", new List<string> { "stores", "Авиакомпания Победа" }, wMsg);
                    return;
                case "newsletters.auchan.ru":
                    Utils.MoveMessage("stores // Auchan", new List<string> { "stores", "Auchan" }, wMsg);
                    return;
                case "bigtv.ru":
                    Utils.MoveMessage("stores // bigtv.ru", new List<string> { "stores", "bigtv.ru" }, wMsg);
                    return;
                case "cardone.org":
                    Utils.MoveMessage("stores // cardone.org", new List<string> { "stores", "cardone.org" }, wMsg);
                    return;
                case "forttd.ru":
                    Utils.MoveMessage("store // TD Fort", new List<string> { "stores", "td_fort" }, wMsg);
                    return;
                case "fotosklad.ru":
                    Utils.MoveMessage("store // Фотосклад", new List<string> { "stores", "Фотосклад" }, wMsg);
                    return;
                case "oldi.ru":
                case "shoppilot.ru":
                    Utils.MoveMessage("store // Oldi", new List<string> { "stores", "oldi.ru" }, wMsg);
                    return;
                case "ozon.ru":
                case "news.ozon.ru":
                    Utils.MoveMessage("store // Ozon", new List<string> { "stores", "Ozon" }, wMsg);
                    return;
                case "megafon.ru":
                case "shop.megafon.ru":
                case "e.shop.megafon.ru":
                    Utils.MoveMessage("store // megafon", new List<string> { "stores", "megafon" }, wMsg);
                    return;
                case "madrobots.ru":
                    Utils.MoveMessage("store // madrobots", new List<string> { "stores", "madrobots" }, wMsg);
                    return;
                case "my-shop.ru":
                    Utils.MoveMessage("store // my-shop.ru", new List<string> { "stores", "my-shop.ru" }, wMsg);
                    return;
                case "onlinetrade.ru":
                    Utils.MoveMessage("store // ОНЛАЙН ТРЕЙД", new List<string> { "stores", "ОНЛАЙН ТРЕЙД" }, wMsg);
                    return;
                case "telepizza-russia.ru":
                    Utils.MoveMessage("store // TelePizza", new List<string> { "stores", "TelePizza" }, wMsg);
                    return;
                case "ulmart.ru":
                case "em.ulmart.ru":
                    Utils.MoveMessage("store // ulmart", new List<string> { "stores", "ulmart" }, wMsg);
                    return;
                case "xcomspb.ru":
                    Utils.MoveMessage("store // XcomSpb", new List<string> { "stores", "XcomSpb" }, wMsg);
                    return;
            }

            if (wMsg.Host.Contains("blablacar.com"))
            {
                Utils.MoveMessage("store // BlaBlaCar", new List<string> { "stores", "BlaBlaCar" }, wMsg);
                return;
            }

            AliExpress(wMsg);
            Avito(wMsg);
        }

        public static void AliExpress(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "promotion@aliexpress.com")
            {
                Utils.MoveMessage("store // AliExpress // Promo", new List<string> { "stores", "AliExpress", "Promo" }, wMsg);
                return;
            }

            if (wMsg.SenderAddress == "transaction@notice.aliexpress.com")
            {
                Utils.MoveMessage("store // AliExpress // Transaction", new List<string> { "stores", "AliExpress", "Transaction" }, wMsg);
                return;
            }
        }

        public static void Avito(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress != "noreply@avito.ru") return;

            switch (wMsg.Message.Subject.ToLower())
            {
                case "вам пришло новое сообщение":
                case "вам пришли новые сообщения":
                    Utils.MoveMessage("store // Avito // New message", new List<string> { "stores", "Avito", "New message" }, wMsg);
                    return;
                case "завтра объявление опубликуется заново":
                    Utils.MoveMessage("store // Avito // Ad renew", new List<string> { "stores", "Avito", "Ad renew" }, wMsg);
                    return;
                case "новые объявления":
                    Utils.MoveMessage("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" }, wMsg);
                    return;
                case "подтверждение подписки на сохраненный поиск":
                    Utils.MoveMessage("store // Avito // Subscription Confirmation", new List<string> { "stores", "Avito", "Subscription Confirmation" }, wMsg);
                    return;
            }

            if (wMsg.Message.Subject.Contains("Персональная подборка автомобилей"))
            {
                Utils.MoveMessage("store // Avito // New ads // Cars", new List<string> { "stores", "Avito", "New ads", "Cars" }, wMsg);
            }
            else if (wMsg.Message.Subject.Contains("Не пропустите подборку интересных объявлений"))
            {
                Utils.MoveMessage("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" }, wMsg);
            }
        }
    }
}
