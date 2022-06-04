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
                    wMsg.Move("store // 05.ru", new List<string> { "stores", "05.ru" });
                    return;
                case "2bit.ru":
                    wMsg.Move("store // 2bit.ru", new List<string> { "stores", "2bit.ru" });
                    return;
                case "5ka.ru":
                case "mail.5ka.ru":
                    wMsg.Move("store // 5", new List<string> { "stores", "5ka.ru" });
                    return;
                case "9814555.ru":
                    wMsg.Move("store // Мопедофф", new List<string> { "stores", "Мопедофф" });
                    return;
                case "auchan.ru":
                case "newsletters.auchan.ru":
                    wMsg.Move("stores // Auchan", new List<string> { "stores", "Auchan" });
                    return;
                case "boxberry.ru":
                    wMsg.Move("stores // Boxberry", new List<string> { "stores", "Boxberry" });
                    return;
                case "lite-mobile.ru":
                    wMsg.Move("store // Lite-Mobile.RU", new List<string> { "stores", "Lite-Mobile.RU" });
                    return;
                case "fotosklad.ru":
                    wMsg.Move("store // Фотосклад", new List<string> { "stores", "Фотосклад" });
                    return;
                case "lentamail.com":
                    wMsg.Move("store // Лента", new List<string> { "stores", "Лента" });
                    return;
                case "madrobots.ru":
                    wMsg.Move("store // madrobots", new List<string> { "stores", "madrobots" });
                    return;
                case "oldi.ru":
                    wMsg.Move("store // Oldi", new List<string> { "stores", "oldi.ru" });
                    return;
                case "onlinetrade.ru":
                    wMsg.Move("store // ОНЛАЙН ТРЕЙД", new List<string> { "stores", "ОНЛАЙН ТРЕЙД" });
                    return;
                case "ozon.ru":
                case "news.ozon.ru":
                    wMsg.Move("store // Ozon", new List<string> { "stores", "Ozon" });
                    return;
                case "pizzaroni.ru":
                    wMsg.Move("store // Pizzaroni", new List<string> { "stores", "Pizzaroni" });
                    return;
                case "pobeda.aero":
                case "info.pobeda.aero":
                    wMsg.Move("store // Авиакомпания Победа", new List<string> { "stores", "Авиакомпания Победа" });
                    return;
                case "samokat.ru":
                    wMsg.Move("store // Самокат", new List<string> { "stores", "Самокат" });
                    return;
                case "telepizza-russia.ru":
                    wMsg.Move("store // TelePizza", new List<string> { "stores", "TelePizza" });
                    return;
                case "xcomspb.ru":
                    wMsg.Move("store // XcomSpb", new List<string> { "stores", "XcomSpb" });
                    return;
            }

            if (wMsg.Host.EndsWith("auchan.ru"))
            {
                wMsg.Move("stores // Auchan", new List<string> { "stores", "Auchan" });
                return;
            }
            else if (wMsg.Host.EndsWith("blablacar.com"))
            {
                wMsg.Move("store // BlaBlaCar", new List<string> { "stores", "BlaBlaCar" });
                return;
            }
            else if (wMsg.Host.EndsWith("ikea.ru"))
            {
                wMsg.Move("store // IKEA", new List<string> { "stores", "IKEA" });
                return;
            }
            else if (wMsg.Host.EndsWith("megafon.ru"))
            {
                wMsg.Move("store // megafon", new List<string> { "stores", "megafon" });
                return;
            }

            AliExpress(wMsg);
            Avito(wMsg);
        }

        public static void AliExpress(WrappedMessage wMsg)
        {
            switch (wMsg.SenderAddress)
            {
                case "promotion@aliexpress.com":
                    wMsg.Move("store // AliExpress // Promo", new List<string> { "stores", "AliExpress", "Promo" });
                    return;
                case "transaction@notice.aliexpress.com":
                    wMsg.Move("store // AliExpress // Transaction", new List<string> { "stores", "AliExpress", "Transaction" });
                    return;
            }

            if (!string.IsNullOrEmpty(wMsg.Message.Subject))
            {
                if (wMsg.Message.Subject.ToLower().Contains("items you liked") ||
                    wMsg.Message.Subject.ToLower().Contains("item you wanted") ||
                    wMsg.Message.Subject.ToLower().Contains("users are interested") ||
                    wMsg.Message.Subject.ToLower().Contains("we miss you") ||
                    wMsg.Message.Subject.ToLower().Contains("being missed") ||
                    wMsg.SenderAddress.Equals("affiliate@notice.aliexpress.com"))
                {
                    wMsg.Delete();
                }
            }
        }

        public static void Avito(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress != "noreply@avito.ru") return;

            switch (wMsg.Message.Subject.ToLower())
            {
                case "вам пришло новое сообщение":
                case "вам пришли новые сообщения":
                    wMsg.Move("store // Avito // New message", new List<string> { "stores", "Avito", "New message" });
                    return;
                case "завтра объявление опубликуется заново":
                    wMsg.Move("store // Avito // Ad renew", new List<string> { "stores", "Avito", "Ad renew" });
                    return;
                case "новые объявления":
                    wMsg.Move("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" });
                    return;
                case "подтверждение подписки на сохраненный поиск":
                    wMsg.Move("store // Avito // Subscription Confirmation", new List<string> { "stores", "Avito", "Subscription Confirmation" });
                    return;
            }

            if (wMsg.Message.Subject.Contains("Персональная подборка автомобилей"))
            {
                wMsg.Move("store // Avito // New ads // Cars", new List<string> { "stores", "Avito", "New ads", "Cars" });
            }
            else if (wMsg.Message.Subject.Contains("Не пропустите подборку интересных объявлений") ||
                     wMsg.Subject.EndsWith("Товары с доставкой") ||
                     wMsg.Subject.EndsWith(" объявлений по вашим поискам"))
            {
                wMsg.Move("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" });
            }
        }
    }
}
