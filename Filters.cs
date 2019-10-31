using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailFilter
{
    internal class Filters
    {
        public static void Atlassian(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var mailAddress = new MailAddress(message.From.Mailboxes.First().Address);
            var host = mailAddress.Host;

            if (host.Contains("atlassian.net"))
            {
                Utils.MoveMessage("Jira", new List<string> { "Jira" }, client, inbox, index);
                return;
            }

            if (host == "bitbucket.org")
            {
                Utils.MoveMessage("Bitbucket", new List<string> { "Bitbucket" }, client, inbox, index);
            }
        }

        public static void Gaemz(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            List<string> GamePublishers = new List<string>
            {
                "battlerite.com",
                "blizzard.com",
                "capcom.com",
                "daybreakgames.com",
                "discordapp.com",
                "ea.com",
                "epicgames.com",
                "goodgame.ru",
                "hirezstudios.com",
                "humblebundle.com",
                "perfectworld.com",
                "steampowered.com",
                "stopgame.ru",
                "twitch.tv",
                "unity3d.com",
                "warframe.com"
            };

            var senderAddress = new MailAddress(message.From.Mailboxes.FirstOrDefault().Address);
            var host = senderAddress.Host;

            switch (host)
            {
                case "crypticstudios.com":
                case "gaijin.net":
                case "playkey.net":
                case "ppy.sh":
                case "email.games2gether.com":
                case "ru.playblackdesert.com":
                    Utils.MoveMessage("gaemz", new List<string> { "gaemz" }, client, inbox, index);
                    return;
                case "gog.com":
                case "email2.gog.com":
                    Utils.MoveMessage("gaemz // GOG", new List<string> { "gaemz", "gog" }, client, inbox, index);
                    return;
                case "riotgames.com":
                case "email.riotgames.com":
                case "riotgames.zendesk.com":
                    Utils.MoveMessage("gaemz // LoL", new List<string> { "gaemz", "LoL" }, client, inbox, index);
                    return;
                case "news.ubisoft.com":
                case "ubi.com":
                case "ubisoft.com":
                    Utils.MoveMessage("gaemz // ubisoft", new List<string> { "gaemz", "Ubisoft" }, client, inbox, index);
                    return;
            }

            foreach (var gamePublisher in GamePublishers)
                if (host.Contains(gamePublisher))
                    Utils.MoveMessage("gaemz", new List<string> { "gaemz", gamePublisher }, client, inbox,
                        index);
        }

        public static void GosUslugi(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            List<string> GosuMailboxes = new List<string> {
                "no-reply@gosuslugi.ru",
                "no_reply@fcod.nalog.ru"
            };

            if (GosuMailboxes.Contains(message.From.Mailboxes.First().Address))
                Utils.MoveMessage("GosUslugi", new List<string> { "ГосУслуги" }, client, inbox, index);
        }

        public static void HeadHunter(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            List<string> HeadHunterMailboxes = new List<string> {
                "noreply@career.ru",
                "no_reply@hh.ru",
                "noreply@hh.ru",
                "no-reply@rabota.ru",
                "noreply@mailer.rabota.ru"
            };

            if (!HeadHunterMailboxes.Contains(message.From.Mailboxes.First().Address)) return;

            switch (message.Subject.ToLower())
            {
                case "вакансия, на которую вы откликались, перенесена в архив":
                case "headhunter: работодатели не знают о вашем резюме":
                case "ваше резюме давно не обновлялось":
                case "ваше резюме просматривали":
                    inbox.AddFlags(index, MessageFlags.Deleted, true);
                    inbox.Expunge();
                    Console.WriteLine("deleted");
                    return;
                case "подходящие вакансии":
                    Utils.MoveMessage("hh // Новые вакансии", new List<string> { "hh", "Новые вакансии" }, client,
                        inbox, index);
                    return;
                case "работодатель не готов пригласить вас на интервью":
                    Utils.MoveMessage("hh // vacancy response rejected :(", new List<string> { "hh", "Потрачено" }, client, inbox, index);
                    return;
                case "ответ на ваше резюме":
                case "предложение о работе":
                    Utils.MoveMessage("hh // Приглашение", new List<string> { "hh", "Приглашения" }, client, inbox, index);
                    return;
                case "сообщение от работодателя":
                case "у вас есть непрочитанные сообщения на rabota.ru!":
                    Utils.MoveMessage("hh // Новое сообщение", new List<string> { "hh", "Новое сообщение" }, client, inbox, index);
                    return;
                case "спасибо за ваше резюме!":
                    Utils.MoveMessage("hh // Прочее", new List<string> { "hh", "Прочие письма" }, client, inbox, index);
                    return;
            }

            if (Regex.Match(message.Subject, @"^Вакансия .+: вам написали из .+$").Success
            )
            {
                Utils.MoveMessage(
                    "hh // Новое сообщение", 
                    new List<string> { "hh", "Новое сообщение" },
                    client, inbox, index
                );
                return;
            }


            if (Regex.Match(message.Subject, @"Новые вакансии \(\d+\) по вашему запросу на сайте").Success ||
                message.Subject.Contains("подходящие вакансии") ||
                message.Subject.Contains("свежие вакансии для вас"))
            {
                Utils.MoveMessage(
                    "hh // Новые вакансии", 
                    new List<string> { "hh", "Новые вакансии" },
                    client, inbox, index
                );
                return;
            }

            if (
                message.Subject.Contains("Приглашение на собеседование") ||
                Regex.Match(message.Subject, @"Ваше резюме на \w*\.?hh.ru интересно работодателю").Success
            )
            {
                Utils.MoveMessage(
                    "hh // Приглашение", 
                    new List<string> { "hh", "Приглашения" },
                    client, inbox, index
                );
                return;
            }

            if (Regex.Match(message.Subject, @"Компания .+ не готова сделать Вам предложение").Success)
                Utils.MoveMessage("hh // vacancy response rejected :(", new List<string> { "hh", "Потрачено" }, client, inbox, index);
        }

        public static void Learnings(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = new MailAddress(message.From.Mailboxes.FirstOrDefault().Address);

            if (senderAddress.Host.Contains("codewars.com"))
                Utils.MoveMessage("Codewars", new List<string> { "learning", "Codewars" }, client, inbox, index);

            if (senderAddress.Host.Contains("coursera.org"))
                Utils.MoveMessage("Coursera", new List<string> { "learning", "Coursera" }, client, inbox, index);

            if (senderAddress.Host.Contains("duolingo.com"))
                Utils.MoveMessage("Duolingo", new List<string> { "learning", "Duolingo" }, client, inbox, index);

            if (senderAddress.Host.Contains("e.mozilla.org"))
                Utils.MoveMessage("Mozilla", new List<string> { "learning", "Mozilla" }, client, inbox, index);
        }

        public static void Megaplan(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = message.From.Mailboxes.FirstOrDefault().Address;
            var senderName = message.From.Mailboxes.FirstOrDefault().Name;
            var host = new MailAddress(senderAddress).Host;

            switch (senderAddress)
            {
                case "weekly@megaplan.ru":
                    Utils.MoveMessage("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" }, client, inbox, index);
                    break;
            }

            switch (senderName)
            {
                case "Мегаплан. Полезное чтение":
                    Utils.MoveMessage("Мегаплан // Полезное чтение", new List<string> { "megaplan", "reading" }, client, inbox, index);
                    break;
            }
        }

        public static void News(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = new MailAddress(message.From.Mailboxes.FirstOrDefault().Address);
            var host = senderAddress.Host;

            switch (host)
            {
                case "amd-member.com":
                    Utils.MoveMessage("News // AMD", new List<string> { "News", "AMD" }, client, inbox, index);
                    return;
                case "auto.ru":
                    Utils.MoveMessage("News // auto.ru", new List<string> { "News", "auto.ru" }, client, inbox, index);
                    return;
                case "f1fanvoice.com":
                    Utils.MoveMessage("News // Formula1", new List<string> { "News", "Formula1" }, client, inbox, index);
                    return;
                case "microsoft.com":
                case "e-mail.microsoft.com":
                    Utils.MoveMessage("News // Microsoft", new List<string> { "News", "Microsoft" }, client, inbox, index);
                    return;
                case "mozilla.org":
                case "e.mozilla.org":
                    Utils.MoveMessage("News // Mozilla", new List<string> { "News", "Mozilla" }, client, inbox, index);
                    return;
                case "navalny.com":
                    Utils.MoveMessage("20!8", new List<string> { "News", "n2018" }, client, inbox, index);
                    return;
                case "tjournal.ru":
                    Utils.MoveMessage("News // Tj", new List<string> { "News", "Tj" }, client, inbox, index);
                    return;
                case "qt.io":
                    Utils.MoveMessage("News // Qt", new List<string> { "News", "Qt" }, client, inbox, index);
                    return;
            }
        }

        public static void Socials(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = new MailAddress(message.From.Mailboxes.FirstOrDefault().Address);
            var host = senderAddress.Host;

            switch (host)
            {
                case "facebookmail.com":
                    Utils.MoveMessage("social // facebook", new List<string> { "social", "facebook" }, client, inbox, index);
                    return;
                case "instagram.com":
                    Utils.MoveMessage("social // instagram", new List<string> { "social", "instagram" }, client, inbox, index);
                    return;
                case "pinterest.com":
                case "explore.pinterest.com":
                case "ideas.pinterest.com":
                case "inspire.pinterest.com":
                    Utils.MoveMessage("social // pinterest", new List<string> { "social", "pinterest" }, client, inbox, index);
                    return;
                case "pixiv.net":
                    Utils.MoveMessage("social // pixiv", new List<string> { "social", "pixiv" }, client, inbox, index);
                    return;
                case "twitter.com":
                    Utils.MoveMessage("social // twitter", new List<string> { "social", "twitter" }, client, inbox, index);
                    return;
                case "vk.com":
                    Utils.MoveMessage("social // vk", new List<string> { "social", "vk" }, client, inbox, index);
                    return;
                case "youtube.com":
                    Utils.MoveMessage("social // youtube", new List<string> { "social", "youtube" }, client, inbox, index);
                    return;
            }
        }

        public static void Stores(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = message.From.Mailboxes.FirstOrDefault().Address;
            var host = (new MailAddress(senderAddress)).Host;

            switch (host)
            {
                case "email.alibaba.com":
                case "info.aliexpress.com":
                case "notice.aliexpress.com":
                    Utils.MoveMessage("store // AliExpress", new List<string> { "stores", "AliExpress" }, client, inbox, index);
                    return;
                case "oldi.ru":
                case "shoppilot.ru":
                    Utils.MoveMessage("store // Oldi", new List<string> { "stores", "oldi.ru" }, client, inbox, index);
                    return;
                case "ozon.ru":
                case "news.ozon.ru":
                    Utils.MoveMessage("store // Ozon", new List<string> { "stores", "Ozon" }, client, inbox, index);
                    return;
                case "shop.megafon.ru":
                case "e.shop.megafon.ru":
                    Utils.MoveMessage("store // megafon", new List<string> { "stores", "megafon" }, client, inbox, index);
                    return;
            }

            switch (senderAddress)
            {
                case "noreply@avito.ru":
                    switch (message.Subject.ToLower())
                    {
                        case "вам пришло новое сообщение":
                            Utils.MoveMessage("store // Avito // New message", new List<string> { "stores", "Avito", "New message" }, client, inbox, index);
                            return;
                        case "новые объявления":
                            Utils.MoveMessage("store // Avito // New ads", new List<string> { "stores", "Avito", "New ads" }, client, inbox, index);
                            return;
                    }
                    return;
            }
        }

        public static void Yandex(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = message.From.Mailboxes.FirstOrDefault().Address;
            var senderName = message.From.Mailboxes.FirstOrDefault().Name;
            var host = new MailAddress(senderAddress).Host;

            switch (senderAddress)
            {
                case "cloud@support.yandex.ru":
                case "CloudPartnerHelp@yandex.ru":
                    Utils.MoveMessage("Yandex // Cloud", new List<string> { "Yandex", "Cloud" },
                        client, inbox, index);
                    break;
                case "events@support.yandex.ru":
                    Utils.MoveMessage("Yandex // Events", new List<string> { "Yandex", "Events" },
                        client, inbox, index);
                    break;
            }

            switch (senderName)
            {
                case "Yandex.Webmaster":
                    Utils.MoveMessage("Yandex // Webmaster", new List<string> { "Yandex", "Webmaster" }, client, inbox, index);
                    break;
                case "Яндекс.Трекер":
                    Utils.MoveMessage("Yandex // Tracker", new List<string> { "Yandex", "Tracker" }, client, inbox, index);
                    break;
            }

            switch (host)
            {
                case "chef.yandex.ru":
                    Utils.MoveMessage("Yandex // Chef", new List<string> { "Yandex", "Chef" }, client, inbox, index);
                    break;
                case "cloud.yandex.ru":
                    Utils.MoveMessage("Yandex // Cloud", new List<string> { "Yandex", "Cloud" }, client, inbox, index);
                    break;
                case "maps.yandex.ru":
                    Utils.MoveMessage("Yandex // Maps", new List<string> { "Yandex", "Maps" }, client, inbox, index);
                    break;
                case "market.yandex.ru":
                    Utils.MoveMessage("Yandex // Market", new List<string> { "Yandex", "Market" }, client, inbox, index);
                    break;
                case "money.yandex.ru":
                    Utils.MoveMessage("Yandex // Money", new List<string> { "Yandex", "Money" }, client, inbox, index);
                    break;
                case "realty.yandex.ru":
                    Utils.MoveMessage("Yandex // Realty", new List<string> { "Yandex", "Realty" }, client, inbox, index);
                    break;
                case "yandex-team.ru":
                    Utils.MoveMessage("Yandex // Support", new List<string> { "Yandex", "Support" }, client, inbox, index);
                    break;
            }
        }

        public static void Tmp(ImapClient client, IMailFolder inbox, MimeMessage message, int index)
        {
            var senderAddress = message.From.Mailboxes.FirstOrDefault().Address;
            var senderName = message.From.Mailboxes.FirstOrDefault().Name;
            var host = new MailAddress(senderAddress).Host;

            switch (senderAddress)
            {
                case "portal@azbukavkusa.ru":
                    Utils.MoveMessage("Tmp", new List<string> { "tmp" }, client, inbox, index);
                    break;
            }
        }
    }
}
