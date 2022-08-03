using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Stores
    {
        public static void Filter(WrappedMessage wMsg)
        {
            var storeDomains = new List<string>() {
                "05.ru",
                "2bit.ru",
                "5ka.ru",
                "auchan.ru",
                "blablacar.com",
                "boxberry.ru",
                "fotosklad.ru",
                "ikea.ru",
                "lite-mobile.ru",
                "madrobots.ru",
                "oldi.ru",
                "onlinetrade.ru",
                "ozon.ru",
                "pizzaroni.ru",
                "samokat.ru",
                "sushiwok.ru",
                "xcomspb.ru"
            };

            foreach (var domain in storeDomains)
            {
                if (wMsg.Host.EndsWith(domain))
                {
                    wMsg.Move($"store // {domain}", new List<string> { "stores", domain });
                    return;
                }
            }

            switch (wMsg.Host)
            {
                case "9814555.ru":
                    wMsg.Move("store // Мопедофф", new List<string> { "stores", "Мопедофф" });
                    return;
                case "lentamail.com":
                    wMsg.Move("store // Лента", new List<string> { "stores", "Лента" });
                    return;
                case "pobeda.aero":
                case "info.pobeda.aero":
                    wMsg.Move("store // Авиакомпания Победа", new List<string> { "stores", "Авиакомпания Победа" });
                    return;
                case "telepizza-russia.ru":
                    wMsg.Move("store // TelePizza", new List<string> { "stores", "TelePizza" });
                    return;
                case "taxcom.ru":
                case "1-ofd.ru":
                case "chek.pofd.ru":
                    wMsg.Move("store // Check", new List<string> { "stores", "checks" });
                    return;
            }

            if (wMsg.Host.EndsWith("megafon.ru"))
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
                     wMsg.Subject.EndsWith("Рекомендации для вас") ||
                     wMsg.Subject.EndsWith("Товары с доставкой") ||
                     wMsg.Subject.EndsWith(" объявлений по вашим поискам"))
            {
                wMsg.Move("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" });
            }
        }
    }
}
