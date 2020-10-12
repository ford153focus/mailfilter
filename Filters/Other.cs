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
        }

        public static void AutoRu(WrappedMessage wMsg)
        {
            if (wMsg.senderAddress != "noreply@auto.ru") return;

            if (!string.IsNullOrEmpty(wMsg.message.Subject) && wMsg.message.Subject.Trim().StartsWith("Новые объявления: "))
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
            if (wMsg.senderAddress == "bee4you@beeline.ru")
            {
                Utils.MoveMessage("Beeline", new List<string> { "beeline" }, wMsg);
                return;
            }
        }

        public static void Cian(WrappedMessage wMsg)
        {
            if (wMsg.host != "cian.ru") return;

            if (wMsg.message.Subject == "Индивидуальная подборка объявлений для вас" ||
                wMsg.message.Subject.StartsWith("Свежие предложения по вашей подписке"))
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
            if (wMsg.senderAddress == "notification@russianpost.ru")
            {
                Utils.MoveMessage("RussianPost", new List<string> { "Почта России" }, wMsg);
                return;
            }

            if (wMsg.senderAddress == "news@pochta.ru")
            {
                Utils.MoveMessage("RussianPost // News", new List<string> { "Почта России", "Новости" }, wMsg);
                return;
            }
        }
    }
}
