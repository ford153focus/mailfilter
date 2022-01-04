using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Other
    {
        public static void Filter(WrappedMessage wMsg)
        {
            Beeline(wMsg);
            Cian(wMsg);
            RussianPost(wMsg);

            if (wMsg.Host == "beget.com" ||
                wMsg.Host == "beget.ltd" ||
                wMsg.Host == "beget.ru")
            {
                wMsg.Move("BeGet", new List<string> { "beget" });
            }
        }

        public static void Beeline(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "bee4you@beeline.ru")
            {
                wMsg.Move("Beeline", new List<string> { "beeline" });
                return;
            }
        }

        public static void Cian(WrappedMessage wMsg)
        {
            if (wMsg.Host != "cian.ru") return;

            if (wMsg.Message.Subject == "Индивидуальная подборка объявлений для вас" ||
                wMsg.Message.Subject.StartsWith("Свежие предложения по вашей подписке"))
            {
                wMsg.Move("stores // cian.ru", new List<string> { "stores", "cian.ru" });
            }
            else
            {
                wMsg.Move("News // cian.ru", new List<string> { "News", "cian.ru" });
            }
        }

        public static void RussianPost(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "notification@russianpost.ru")
            {
                wMsg.Move("RussianPost", new List<string> { "Почта России" });
                return;
            }

            if (wMsg.SenderAddress == "news@pochta.ru")
            {
                wMsg.Move("RussianPost // News", new List<string> { "Почта России", "Новости" });
                return;
            }
        }
    }
}
