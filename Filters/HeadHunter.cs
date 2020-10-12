using System.Collections.Generic;
using System.Text.RegularExpressions;
using MailKit;

namespace MailFilter.Filters
{
    internal class HeadHunter
    {
        public static void Filter(WrappedMessage wMsg)
        {
            List<string> HeadHunterMailboxes = new List<string> {
                "auth@site.hh.ru",
                "no_reply@hh.ru",
                "no-reply@rabota.ru",
                "noreply@career.ru",
                "noreply@hh.ru",
                "noreply@hh.ua",
                "noreply@mailer.rabota.ru",
            };

            if (!HeadHunterMailboxes.Contains(wMsg.senderAddress)) return;

            switch (wMsg.message.Subject.ToLower())
            {
                case "вы искали похожие вакансии":
                case "подходящие вакансии":
                    Utils.MoveMessage("hh // Новые вакансии", new List<string> { "hh", "Новые вакансии" }, wMsg);
                    return;
                case "работодатель не готов пригласить вас на интервью":
                case "работодатель не готов сделать вам предложение":
                    Utils.MoveMessage("hh // rejected :(", new List<string> { "hh", "Потрачено" }, wMsg);
                    return;
                case "ответ на ваше резюме":
                case "предложение о работе":
                    Utils.MoveMessage("hh // Приглашение", new List<string> { "hh", "Приглашения" }, wMsg);
                    return;
                case "сообщение от работодателя":
                case "у вас есть непрочитанные сообщения на rabota.ru!":
                    Utils.MoveMessage("hh // Новое сообщение", new List<string> { "hh", "Новое сообщение" }, wMsg);
                    return;
                case "спасибо за ваше резюме!":
                    Utils.MoveMessage("hh // Прочее", new List<string> { "hh", "Прочие письма" }, wMsg);
                    return;
            }

            if (
                wMsg.message.Subject.Contains("ваше резюме давно не обновлялось") ||
                wMsg.message.Subject.Contains("обновите резюме") ||
                wMsg.message.Subject.Contains("работодатели не знают о вашем резюме")
            )
            {
                Utils.MoveMessage("hh // Bump your resume", new List<string> { "hh", "bump_your_resume" }, wMsg);
                return;
            }

            if (
                wMsg.message.Subject.Contains("ваше резюме просматривали") ||
                (wMsg.message.Subject.Contains("вакансия") && wMsg.message.Subject.Contains("перенесена в архив"))
            )
            {
                wMsg.inbox.AddFlags(wMsg.index, MessageFlags.Deleted, true);
                wMsg.inbox.Expunge();
                Utils.ErrorWrite("deleted");
                return;
            }

            if (Regex.Match(wMsg.message.Subject, @"^Вакансия .+: вам написали из .+$").Success)
            {
                Utils.MoveMessage("hh // Новое сообщение", new List<string> { "hh", "Новое сообщение" }, wMsg);
                return;
            }


            if (Regex.Match(wMsg.message.Subject, @"Новые вакансии \(\d+\) по вашему запросу на сайте").Success ||
                Regex.Match(wMsg.message.Subject, @".+, новые вакансии \(\d+\) по вашему запросу на сайте").Success ||
                wMsg.message.Subject.Contains("подходящие вакансии") ||
                wMsg.message.Subject.Contains("свежие вакансии для вас"))
            {
                Utils.MoveMessage("hh // Новые вакансии", new List<string> { "hh", "Новые вакансии" }, wMsg);
                return;
            }

            if (
                wMsg.message.Subject.Contains("Приглашение на собеседование") ||
                Regex.Match(wMsg.message.Subject, @"Ваше резюме на \w*\.?hh.ru интересно работодателю").Success
            )
            {
                Utils.MoveMessage(
                    "hh // Приглашение",
                    new List<string> { "hh", "Приглашения" }, wMsg);
                return;
            }

            if (Regex.Match(wMsg.message.Subject, @"Компания .+ не готова сделать Вам предложение").Success)
                Utils.MoveMessage("hh // vacancy response rejected :(", new List<string> { "hh", "Потрачено" }, wMsg);

            Utils.MoveMessage("hh // unknown", new List<string> { "hh", "Прочие письма" }, wMsg);
        }
    }
}
