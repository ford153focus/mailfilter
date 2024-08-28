using System.Collections.Generic;

namespace MailFilter.Filters;

internal class Yandex
{
    public static void AutoRu(WrappedMessage wMsg)
    {
        if (wMsg.SenderAddress != "noreply@auto.ru" &&
            wMsg.SenderAddress != "mag@auto.ru") return;

        if (!string.IsNullOrEmpty(wMsg.Message.Subject) && wMsg.Message.Subject.Trim().StartsWith("Новые объявления: "))
        {
            wMsg.Move("stores // auto.ru / new ads", new List<string> { "stores", "auto.ru", "new ads" });
        }
        else
        {
            wMsg.Move("News // auto.ru", new List<string> { "news", "auto.ru" });
        }
    }

    public static void Filter(WrappedMessage wMsg)
    {
        AutoRu(wMsg);

        if (wMsg.Message.Subject.Contains("Yandex Cup"))
        {
            wMsg.Move("Yandex // Cup", new List<string> { "Yandex", "Cup" });
        }

        switch (wMsg.SenderAddress)
        {
            case "cloud@support.yandex.ru":
            case "CloudPartnerHelp@yandex.ru":
                wMsg.Move("Yandex // Cloud", new List<string> { "Yandex", "Cloud" });
                break;
            case "events@support.yandex.ru":
                wMsg.Move("Yandex // Events", new List<string> { "Yandex", "Events" });
                break;
            case "noreply@eda.yandex.ru":
                wMsg.Move("Yandex // Eda", new List<string> { "Yandex", "Eda" });
                break;
            case "intern@yandex-team.ru":
                wMsg.Move("Yandex // Школа", new List<string> { "Yandex", "Школа" });
                break;
        }

        switch (wMsg.SenderName)
        {
            case "Yandex.Webmaster":
            case "Яндекс.Вебмастер":
                wMsg.Move("Yandex // Webmaster", new List<string> { "Yandex", "Webmaster" });
                break;
            case "Yandex.Connect":
            case "Яндекс.Коннект":
                wMsg.Move("Yandex // Connect", new List<string> { "Yandex", "Connect" });
                break;
            case "Яндекс.Карты":
                wMsg.Move("Yandex // Maps", new List<string> { "Yandex", "Maps" });
                break;
            case "Яндекс.Трекер":
                wMsg.Move("Yandex // Tracker", new List<string> { "Yandex", "Tracker" });
                break;
        }

        switch (wMsg.Host)
        {
            case "360.yandex.ru":
                wMsg.Move("Yandex // 360", new List<string> { "Yandex", "360" });
                break;
            case "chef.yandex.ru":
                wMsg.Move("Yandex // Chef", new List<string> { "Yandex", "Chef" });
                break;
            case "cloud.yandex.ru":
                wMsg.Move("Yandex // Cloud", new List<string> { "Yandex", "Cloud" });
                break;
            case "disk.yandex.ru":
                wMsg.Move("Yandex // Disk", new List<string> { "Yandex", "Disk" });
                break;
            case "kinopoisk.ru":
                wMsg.Move("Yandex // КиноПоиск", new List<string> { "Yandex", "КиноПоиск" });
                break;
            case "maps.yandex.ru":
                wMsg.Move("Yandex // Maps", new List<string> { "Yandex", "Maps" });
                break;
            case "market.yandex.ru":
                wMsg.Move("Yandex // Market", new List<string> { "Yandex", "Market" });
                break;
            case "metrika.yandex.ru":
                wMsg.Move("Yandex // Metrika", new List<string> { "Yandex", "Metrika" });
                break;
            case "realty.yandex.ru":
                wMsg.Move("Yandex // Realty", new List<string> { "Yandex", "Realty" });
                break;
        }
    }
}