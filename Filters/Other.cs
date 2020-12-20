using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Other
    {
        public static void Filter(WrappedMessage wMsg)
        {
            AutoRu(wMsg);
            Beeline(wMsg);
            Cian(wMsg);
            RussianPost(wMsg);

            if (wMsg.Host == "beget.ru") { Utils.MoveMessage("BeGet", new List<string> { "beget" }, wMsg); }
        }

        public static void AutoRu(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress != "noreply@auto.ru") return;

            if (!string.IsNullOrEmpty(wMsg.Message.Subject) && wMsg.Message.Subject.Trim().StartsWith("Новые объявления: "))
            {
                Utils.MoveMessage("stores // auto.ru / new ads", new List<string> { "Stores", "auto.ru", "new ads" }, wMsg);
            }
            else
            {
                Utils.MoveMessage("News // auto.ru", new List<string> { "News", "auto.ru" }, wMsg);
            }
        }

        public static void Beeline(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "bee4you@beeline.ru")
            {
                Utils.MoveMessage("Beeline", new List<string> { "beeline" }, wMsg);
                return;
            }
        }

        public static void Cian(WrappedMessage wMsg)
        {
            if (wMsg.Host != "cian.ru") return;

            if (wMsg.Message.Subject == "Индивидуальная подборка объявлений для вас" ||
                wMsg.Message.Subject.StartsWith("Свежие предложения по вашей подписке"))
            {
                Utils.MoveMessage("stores // cian.ru", new List<string> { "stores", "cian.ru" }, wMsg);
            }
            else
            {
                Utils.MoveMessage("News // cian.ru", new List<string> { "News", "cian.ru" }, wMsg);
            }
        }

        public static void RussianPost(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "notification@russianpost.ru")
            {
                Utils.MoveMessage("RussianPost", new List<string> { "Почта России" }, wMsg);
                return;
            }

            if (wMsg.SenderAddress == "news@pochta.ru")
            {
                Utils.MoveMessage("RussianPost // News", new List<string> { "Почта России", "Новости" }, wMsg);
                return;
            }
        }
    }
}
