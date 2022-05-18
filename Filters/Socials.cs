using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Socials
    {
        public static void LinkedIn(WrappedMessage wMsg)
        {
            if (wMsg.Message.Subject == "Я приглашаю вас установить контакт")
            {
                wMsg.Move("social // LinkedIn // add me", new List<string> { "social", "LinkedIn", "add me" });
            }
            else if (wMsg.Message.Subject.StartsWith("Приглашение участника") && wMsg.Message.Subject.EndsWith("ожидает Вашего ответа"))
            {
                wMsg.Move("social // LinkedIn // add me", new List<string> { "social", "LinkedIn", "add me" });
            }
            else if (wMsg.Message.Subject.Contains("добавьте участника") && wMsg.Message.Subject.EndsWith("в свою сеть контактов"))
            {
                wMsg.Move("social // LinkedIn // add me", new List<string> { "social", "LinkedIn", "add me" });
            }
            else if (wMsg.Message.Subject.EndsWith("только что отправил(а) Вам сообщение"))
            {
                wMsg.Move("social // LinkedIn // new message", new List<string> { "social", "LinkedIn", "new message" });
            }
            else if (wMsg.Message.Subject.StartsWith("Ваш профиль появлялся в результатах поиска"))
            {
                wMsg.Move("social // LinkedIn // profile appeared in search", new List<string> { "social", "LinkedIn", "profile appeared in search" });
            }
            else if (wMsg.Message.Subject.Contains("ищет нового сотрудника"))
            {
                wMsg.Move("social // LinkedIn // company X searching new employee", new List<string> { "social", "LinkedIn", "company X searching new employee" });
            }
            else if (wMsg.Message.Subject.Contains("начните общение со своим новым контактом"))
            {
                wMsg.Move("social // LinkedIn // contact added", new List<string> { "social", "LinkedIn", "contact added" });
            }
            else
            {
                wMsg.Move("social // LinkedIn", new List<string> { "social", "LinkedIn" });
            }

        }

        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.Host)
            {
                case "facebookmail.com":
                    wMsg.Move("social // facebook", new List<string> { "social", "facebook" });
                    return;
                case "mail.instagram.com":
                case "instagram.com":
                    wMsg.Move("social // instagram", new List<string> { "social", "instagram" });
                    return;
                case "linkedin.com":
                    LinkedIn(wMsg);
                    return;
                case "pixiv.net":
                    wMsg.Move("social // pixiv", new List<string> { "social", "pixiv" });
                    return;
                case "twitter.com":
                    wMsg.Move("social // twitter", new List<string> { "social", "twitter" });
                    return;
                case "vk.com":
                case "notify.vk.com":
                    wMsg.Move("social // vk", new List<string> { "social", "vk" });
                    return;
                case "youtube.com":
                    wMsg.Move("social // youtube", new List<string> { "social", "youtube" });
                    return;
            }

            if (wMsg.Host.EndsWith("github.com"))
            {
                wMsg.Move("social // GitHub", new List<string> { "social", "GitHub" });
            }
            else if (wMsg.Host.EndsWith("joyreactor.cc"))
            {
                wMsg.Move("social // Joyreactor", new List<string> { "social", "Joyreactor" });
            }
            else if (wMsg.Host.EndsWith("habr.com") && wMsg.Message.Subject.StartsWith("Ответ на ваш комментарий к публикации"))
            {
                wMsg.Move("social // Habr", new List<string> { "social", "Habr" });
            }
            else if (wMsg.Host.EndsWith("pinterest.com"))
            {
                wMsg.Move("social // pinterest", new List<string> { "social", "pinterest" });
            }
            else if (wMsg.Host.EndsWith("redditmail.com"))
            {
                wMsg.Move("social // Reddit", new List<string> { "social", "Reddit" });
            }
            else if (wMsg.Host.EndsWith("stackoverflow.email"))
            {
                wMsg.Move("social // Stack Overflow", new List<string> { "social", "Stack Overflow" });
            }
        }
    }
}
