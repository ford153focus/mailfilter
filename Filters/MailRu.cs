using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class MailRu
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (!wMsg.Host.EndsWith("mail.ru")) return;

            switch (wMsg.SenderName)
            {
                case "Certification":
                    wMsg.Move("Mail.ru // Certification", new List<string> { "Mail.ru", "Certification" });
                    break;
                case "Облако Mail.ru":
                    wMsg.Move("Mail.ru // Облако", new List<string> { "Mail.ru", "Облако" });
                    break;
                case "Ответы@Mail.Ru":
                    wMsg.Move("Mail.ru // Ответы", new List<string> { "Mail.ru", "Ответы" });
                    break;
                case "Почта Mail.ru":
                    wMsg.Move("Mail.ru // Почта", new List<string> { "Mail.ru", "Почта" });
                    break;
            }
        }
    }
}
