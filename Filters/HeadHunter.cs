using System.Collections.Generic;
using System.Text.RegularExpressions;
using MailKit;

namespace MailFilter.Filters
{
    internal class HeadHunter
    {
        public static void Filter(WrappedMessage wMsg)
        {
            List<string> headHunterMailboxes = new List<string> {
                "auth@site.hh.ru",
                "no_reply@hh.ru",
                "no-reply@rabota.ru",
                "noreply@career.ru",
                "noreply@hh.ru",
                "noreply@hh.ua",
                "noreply@mailer.rabota.ru",
            };

            if (!headHunterMailboxes.Contains(wMsg.SenderAddress)) return;

            switch (wMsg.Message.Subject.ToLower())
            {
                case "вы искали похожие вакансии":
                case "подходящие вакансии":
                    wMsg.Move("hh // Новые вакансии", new List<string> { "hh", "Новые вакансии" });
                    return;
                case "работодатель не готов пригласить вас на интервью":
                case "работодатель не готов сделать вам предложение":
                    wMsg.Move("hh // rejected :(", new List<string> { "hh", "Потрачено" });
                    return;
                case "ответ на ваше резюме":
                case "предложение о работе":
                    wMsg.Move("hh // invitation", new List<string> { "hh", "invitation" });
                    return;
                case "сообщение от работодателя":
                case "у вас есть непрочитанные сообщения на rabota.ru!":
                    wMsg.Move("hh // Новое сообщение", new List<string> { "hh", "Новое сообщение" });
                    return;
                case "спасибо за ваше резюме!":
                    wMsg.Move("hh // Прочее", new List<string> { "hh", "Прочие письма" });
                    return;
            }

            if (
                wMsg.Message.Subject.Contains("ваше резюме давно не обновлялось") ||
                wMsg.Message.Subject.Contains("обновите резюме") ||
                wMsg.Message.Subject.Contains("работодатели не знают о вашем резюме")
            )
            {
                wMsg.Move("hh // Bump your resume", new List<string> { "hh", "bump_your_resume" });
                return;
            }

            if (
                wMsg.Message.Subject.Contains("ваше резюме просматривали") ||
                (wMsg.Message.Subject.Contains("вакансия") && wMsg.Message.Subject.Contains("перенесена в архив"))
            )
            {
                wMsg.Delete();
                return;
            }

            if (Regex.Match(wMsg.Message.Subject, @"^Вакансия .+: вам написали из .+$").Success)
            {
                wMsg.Move("hh // Новое сообщение", new List<string> { "hh", "Новое сообщение" });
                return;
            }


            if (Regex.Match(wMsg.Message.Subject, @"Новые вакансии \(\d+\) по вашему запросу на сайте").Success ||
                Regex.Match(wMsg.Message.Subject, @".+, новые вакансии \(\d+\) по вашему запросу на сайте").Success ||
                wMsg.Message.Subject.Contains("подходящие вакансии") ||
                wMsg.Message.Subject.Contains("свежие вакансии для вас"))
            {
                wMsg.Move("hh // Новые вакансии", new List<string> { "hh", "Новые вакансии" });
                return;
            }

            if (
                wMsg.Message.Subject.Contains("Приглашение на собеседование") ||
                Regex.Match(wMsg.Message.Subject, @"Ваше резюме на \w*\.?hh.ru интересно работодателю").Success
            )
            {
                wMsg.Move("hh // invitation", new List<string> { "hh", "invitation" });
                return;
            }

            if (Regex.Match(wMsg.Message.Subject, @"Компания .+ не готова сделать Вам предложение").Success)
                wMsg.Move("hh // vacancy response rejected :(", new List<string> { "hh", "Потрачено" });

            wMsg.Move("hh // unknown", new List<string> { "hh", "Прочие письма" });
        }
    }
}
