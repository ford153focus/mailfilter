using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Mailfilter {
    class Filters {
        private static List<String> gamePublishers = new List<String> () {
            "battlerite.com",
            "blizzard.com",
            "capcom.com",
            "daybreakgames.com",
            "discordapp.com",
            "ea.com",
            "epicgames.com",
            "goodgame.ru",
            "gog.com",
            "hirezstudios.com",
            "humblebundle.com",
            "perfectworld.com",
            "riotgames.com",
            "steampowered.com",
            "stopgame.ru",
            "twitch.tv",
            "ubi.com",
            "ubisoft.com",
            "unity3d.com",
            "warframe.com",
        };

        private static List<String> GosuMailboxes = new List<String> () {
            "no-reply@gosuslugi.ru",
            "no_reply@fcod.nalog.ru",
        };

        private static List<String> headHunterMailboxes = new List<String> () {
            "noreply@career.ru",
            "no_reply@hh.ru",
            "noreply@hh.ru",
            "no-reply@rabota.ru",
            "noreply@mailer.rabota.ru"
        };

        private static List<String> socialNetworkDomains = new List<String> () {
            "facebookmail.com",
            "instagram.com",
            "pinterest.com",
            "pixiv.net",
            "twitter.com",
            "vk.com"
        };

        private static List<String> storesDomains = new List<String> () {
            "oldi.ru",
            "ozon.ru"
        };

        public static void Atlassian (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var mailAddress = new MailAddress (message.From.Mailboxes.First ().Address);
            var host = mailAddress.Host;

            if (host.Contains ("atlassian.net")) {
                Utils.MoveMessage ("Jira", (new List<String> () { "Jira" }), client, inbox, index);
                return;
            }

            if (host == "bitbucket.org") {
                Utils.MoveMessage ("Bitbucket", (new List<String> () { "Bitbucket" }), client, inbox, index);
            }
        }

        public static void Gaemz (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);
            var host = senderAddress.Host;

            if (host == "crypticstudios.com") {
                Utils.MoveMessage ("gaemz", (new List<String> () { "gaemz" }), client, inbox, index);
                return;
            }

            if (host == "ppy.sh") {
                Utils.MoveMessage ("gaemz // osu!", (new List<String> () { "gaemz", "osu!" }), client, inbox, index);
                return;
            }

            if (host == "riotgames.zendesk.com" ||
                host == "riotgames.com") {
                Utils.MoveMessage ("gaemz // LoL", (new List<String> () { "gaemz", "LoL" }), client, inbox, index);
                return;
            }

            foreach (var gamePublisher in gamePublishers) {
                if (host.Contains (gamePublisher.ToString ())) {
                    Utils.MoveMessage ("gaemz", (new List<String> () { "gaemz", gamePublisher.ToString () }), client, inbox, index);
                }
            }
        }

        public static void GosUslugi (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            if (GosuMailboxes.Contains (message.From.Mailboxes.First ().Address)) {
                Utils.MoveMessage ("GosUslugi", (new List<String> () { "ГосУслуги" }), client, inbox, index);
            }
        }

        public static void HeadHunter (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            if (headHunterMailboxes.Contains (message.From.Mailboxes.First ().Address)) {
                switch (message.Subject.ToLower())
                {
                    case "вакансия, на которую вы откликались, перенесена в архив":
                    case "headhunter: работодатели не знают о вашем резюме":
                    case "ваше резюме давно не обновлялось":
                    case "ваше резюме просматривали":
                        inbox.AddFlags (index, MessageFlags.Deleted, true);
                        inbox.Expunge ();
                        Console.WriteLine ("deleted");
                        return;
                    case "подходящие вакансии":
                        Utils.MoveMessage ("hh // Новые вакансии", (new List<String> () { "hh", "Новые вакансии" }), client, inbox, index);
                        return;
                    case "работодатель не готов пригласить вас на интервью":
                        Utils.MoveMessage ("hh // vacancy response rejected :(", (new List<String> () { "hh", "Потрачено" }), client, inbox, index);
                        return;
                    case "ответ на ваше резюме":
                    case "предложение о работе":
                        Utils.MoveMessage ("hh // Приглашение", (new List<String> () { "hh", "Приглашения" }), client, inbox, index);
                        return;
                    case "сообщение от работодателя":
                    case "у вас есть непрочитанные сообщения на rabota.ru!":
                        Utils.MoveMessage ("hh // Новое сообщение", (new List<String> () { "hh", "Новое сообщение" }), client, inbox, index);
                        return;
                    case "спасибо за ваше резюме!":
                        Utils.MoveMessage ("hh // Прочее", (new List<String> () { "hh", "Прочие письма" }), client, inbox, index);
                        return;
                }

                if ((Regex.Match (message.Subject, @"^Вакансия .+: вам написали из .+$")).Success
                ) {
                    Utils.MoveMessage ("hh // Новое сообщение", (new List<String> () { "hh", "Новое сообщение" }), client, inbox, index);
                    return;
                }


                if ((Regex.Match (message.Subject, @"Новые вакансии \(\d+\) по вашему запросу на сайте")).Success ||
                    message.Subject.Contains ("свежие вакансии для вас")) {
                    Utils.MoveMessage ("hh // Новые вакансии", (new List<String> () { "hh", "Новые вакансии" }), client, inbox, index);
                    return;
                }

                if (message.Subject.Contains ("Приглашение на собеседование") ||
                    (Regex.Match (message.Subject, @"Ваше резюме на \w*\.?hh.ru интересно работодателю")).Success
                ) {
                    Utils.MoveMessage ("hh // Приглашение", (new List<String> () { "hh", "Приглашения" }), client, inbox, index);
                    return;
                }

                if ((Regex.Match (message.Subject, @"Компания .+ не готова сделать Вам предложение")).Success) {
                    Utils.MoveMessage ("hh // vacancy response rejected :(", (new List<String> () { "hh", "Потрачено" }), client, inbox, index);
                    return;
                }
            }
        }

        public static void Learnings (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);

            if (senderAddress.Host.Contains ("codewars.com")) {
                Utils.MoveMessage ("Codewars", (new List<String> () { "learning", "Codewars" }), client, inbox, index);
            }

            if (senderAddress.Host.Contains ("coursera.org")) {
                Utils.MoveMessage ("Coursera", (new List<String> () { "learning", "Coursera" }), client, inbox, index);
            }

            if (senderAddress.Host.Contains ("duolingo.com")) {
                Utils.MoveMessage ("Duolingo", (new List<String> () { "learning", "Duolingo" }), client, inbox, index);
            }

            if (senderAddress.Host.Contains ("e.mozilla.org")) {
                Utils.MoveMessage ("Mozilla", (new List<String> () { "learning", "Mozilla" }), client, inbox, index);
            }
        }

        public static void News (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);
            var host = senderAddress.Host;

            if (host.Contains ("e.mozilla.org")) {
                Utils.MoveMessage ("News // Mozilla", (new List<String> () { "News", "Mozilla" }), client, inbox, index);
            }

            if (host == "f1fanvoice.com") {
                Utils.MoveMessage ("News // Formula1", (new List<String> () { "News", "Formula1" }), client, inbox, index);
            }

            if (host == "qt.io") {
                Utils.MoveMessage ("News // Qt", (new List<String> () { "News", "Qt" }), client, inbox, index);
            }

            if (message.From.Mailboxes.First ().Address == "shtab@navalny.com") {
                Utils.MoveMessage ("20!8", (new List<String> () { "News", "n2018" }), client, inbox, index);
            }
        }

        public static void Socials (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);
            var host = senderAddress.Host;

            foreach (var socialProvider in socialNetworkDomains) {
                if (host.Contains (socialProvider.ToString ())) {
                    Utils.MoveMessage ("Socials", (new List<String> () { "Социалочки", socialProvider.ToString () }), client, inbox, index);
                }
            }
        }

        public static void Stores (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);
            var host = senderAddress.Host;

            switch (host)
            {
                case "shoppilot.ru":
                    Utils.MoveMessage ("store // Oldi", (new List<String> () { "stores", "oldi.ru" }), client, inbox, index);
                    return;
                case "e.shop.megafon.ru":
                    Utils.MoveMessage ("store // megafon", (new List<String> () { "stores", "megafon" }), client, inbox, index);
                    return;
            }

            foreach (var storeDomain in storesDomains) {
                if (host.Contains (storeDomain.ToString ())) {
                    Utils.MoveMessage ("store // " + host, (new List<String> () { "stores", storeDomain.ToString () }), client, inbox, index);
                }
            }
        }

        public static void Yandex (ImapClient client, IMailFolder inbox, MimeMessage message, int index) {
            var senderAddress = new MailAddress (message.From.Mailboxes.FirstOrDefault ().Address);
            var host = senderAddress.Host;

            switch (host) {
                case "cloud.yandex.ru":
                    Utils.MoveMessage ("Yandex // Cloud", (new List<String> () { "Yandex", "Cloud" }), client, inbox, index);
                    break;
                case "maps.yandex.ru":
                    Utils.MoveMessage ("Yandex // Maps", (new List<String> () { "Yandex", "Maps" }), client, inbox, index);
                    break;
                case "money.yandex.ru":
                    Utils.MoveMessage ("Yandex // Money", (new List<String> () { "Yandex", "Money" }), client, inbox, index);
                    break;
                case "realty.yandex.ru":
                    Utils.MoveMessage ("Yandex // Realty", (new List<String> () { "Yandex", "Realty" }), client, inbox, index);
                    break;
            }
        }
    }
}