using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MailFilter.Filters
{
    internal class Gaemz
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.Host)
            {
                case "amplitude-studios.com":
                case "battlerite.com":
                case "crypticstudios.com":
                case "email.games2gether.com":
                case "gaijin.net":
                case "mobafire.com":
                case "playkey.net":
                    wMsg.Move("gaemz // Other", new List<string> { "gaemz", "Other" });
                    return;
                case "4game.com":
                    wMsg.Move("gaemz // 4game", new List<string> { "gaemz", "4game" });
                    return;
                 case "ru.playblackdesert.com":
                 case "pearlabyss.com":
                    wMsg.Move("gaemz // bdesert", new List<string> { "gaemz", "black_desert" });
                    return;
                case "blizzard.com":
                case "em.blizzard.com":
                    wMsg.Move("gaemz // Blizzard", new List<string> { "gaemz", "Blizzard" });
                    return;
                case "capcom.com":
                    wMsg.Move("gaemz // Capcom", new List<string> { "gaemz", "Capcom" });
                    return;
                case "discordapp.com":
                    wMsg.Move("gaemz // Discord", new List<string> { "gaemz", "Discord" });
                    return;
                case "daybreakgames.com":
                case "email.daybreakgames.com":
                    wMsg.Move("gaemz // H1Z1", new List<string> { "gaemz", "H1Z1" });
                    return;
                case "ea.com":
                case "e.ea.com":
                    wMsg.Move("gaemz // EA", new List<string> { "gaemz", "EA" });
                    return;
                case "eslgaming.com":
                    wMsg.Move("gaemz // ESL", new List<string> { "gaemz", "ESL" });
                    return;
                case "goodgame.ru":
                    wMsg.Move("gaemz // GoodGame", new List<string> { "gaemz", "GoodGame" });
                    return;
                case "hirezstudios.com":
                    wMsg.Move("gaemz // Hi-Rez Studios", new List<string> { "gaemz", "Hi-Rez Studios" });
                    return;
                case "klavogonki.ru":
                    wMsg.Move("gaemz // Клавогонки", new List<string> { "gaemz", "Клавогонки" });
                    return;
                case "perfectworld.com":
                    wMsg.Move("gaemz // Perfect World", new List<string> { "gaemz", "Perfect World" });
                    return;
                case "ppy.sh":
                    wMsg.Move("gaemz // OSU!", new List<string> { "gaemz", "osu" });
                    return;
                case "robotcache.com":
                    wMsg.Move("gaemz // Robot Cache", new List<string> { "gaemz", "Robot Cache" });
                    return;
                case "rockstargames.com":
                    wMsg.Move("gaemz // Rockstar Games", new List<string> { "gaemz", "Rockstar Games" });
                    return;
                case "stopgame.ru":
                    wMsg.Move("gaemz // StopGame.ru", new List<string> { "gaemz", "StopGame.ru" });
                    return;
                case "unity3d.com":
                    wMsg.Move("gaemz // Unity 3D Engine", new List<string> { "gaemz", "Unity 3D Engine" });
                    return;
                case "wasd.tv":
                    wMsg.Move("gaemz // WASD.TV", new List<string> { "gaemz", "WASD.TV" });
                    return;
            }

            if (wMsg.Host.EndsWith("epicgames.com"))
            {
                if (wMsg.Message.Subject.StartsWith("Your Epic Games Receipt"))
                {
                    wMsg.Move("gaemz // Epic Games // Receipt", new List<string> { "gaemz", "Epic Games", "Receipt" });
                }
                else
                {
                    wMsg.Move("gaemz // Epic Games", new List<string> { "gaemz", "Epic Games" });
                }
            }
            else if (wMsg.Host.EndsWith("gog.com"))
            {
                switch (wMsg.Message.Subject)
                {
                    case "Items on your wishlist are now discounted!":
                    case "На игры из вашего вишлиста сейчас действуют скидки":
                        wMsg.Move("gaemz // GOG.com // discount", new List<string> { "gaemz", "GOG.com", "discount" });
                        break;
                    case "Free items added to your GOG.com library.":
                    case "Бесплатные продукты были добавлены в вашу библиотеку GOG.com.":
                        wMsg.Move("gaemz // GOG.com // free game added", new List<string> { "gaemz", "GOG.com", "free game added" });
                        break;
                    default:
                        wMsg.Move("gaemz // GOG.com", new List<string> { "gaemz", "GOG.com" });
                        break;
                }
            }
            else if (wMsg.Host.EndsWith("humblebundle.com"))
            {
                wMsg.Move("gaemz // Humble Bundle", new List<string> { "gaemz", "Humble Bundle" });
            }
            else if (wMsg.Host.Contains("riotgames"))
            {
                wMsg.Move("gaemz // LoL", new List<string> { "gaemz", "LoL" });
            }
            else if (wMsg.Host.EndsWith("twitch.tv"))
            {
                wMsg.Move("gaemz // Twitch", new List<string> { "gaemz", "Twitch" });
            }
            else if (wMsg.Host.Contains("ubi"))
            {
                wMsg.Move("gaemz // Ubisoft", new List<string> { "gaemz", "Ubisoft" });
            }
            else if (wMsg.Host.EndsWith("warframe.com"))
            {
                wMsg.Move("gaemz // Warframe", new List<string> { "gaemz", "Warframe" });
            }
            else if (wMsg.Host.EndsWith("mihoyo.com"))
            {
                wMsg.Move("gaemz // Genshin Impact", new List<string> { "gaemz", "Genshin Impact" });
            }
            else
            {
                Steam(wMsg);
            }
        }

        public static void Steam(WrappedMessage wMsg)
        {
            if (wMsg.Host != "steampowered.com") return;

            // Community Market Purchase
            if (wMsg.Message.Subject == "Thank you for your Community Market purchase")
            {
                wMsg.Move("gaemz // Steam // Community Market Purchase", new List<string> { "gaemz", "Steam", "Community Market Purchase" });
                return;
            }

            // Gift received
            if (wMsg.Message.Subject.Contains("You've received a gift copy of the game ") ||
                wMsg.Message.Subject.Contains("Вы получили в подарок копию игры "))
            {
                wMsg.Move("gaemz // Steam // Gift received", new List<string> { "gaemz", "Steam", "Gift received" });
                return;
            }

            // Item is sold on the Community Market
            if (wMsg.Message.Subject == "You have sold an item on the Community Market" ||
                wMsg.Message.Subject == "Вы продали предмет на Торговой площадке")
            {
                wMsg.Move("gaemz // Steam // Item is sold on the Community Market", new List<string> { "gaemz", "Steam", "Item is sold on the Community Market" });
                return;
            }

            // Items Delivered
            if (wMsg.Message.Subject == "Steam Trade Items Delivered")
            {
                wMsg.Move("gaemz // Steam // Trade Items Delivered", new List<string> { "gaemz", "Steam", "Trade Items Delivered" });
                return;
            }

            // Released
            if (wMsg.Message.Subject.Contains(" is now available on Steam!") ||
                wMsg.Message.Subject.Contains(" is now available in Early Access on Steam!") ||
                wMsg.Message.Subject.Contains(" уже доступна в Steam!"))
            {
                wMsg.Move("gaemz // Steam // Released", new List<string> { "gaemz", "Steam", "Released" });
                return;
            }

            // Support
            if (wMsg.SenderName == "Steam Support")
            {
                wMsg.Move("gaemz // Steam // Support", new List<string> { "gaemz", "Steam", "Support" });
                return;
            }

            // Trade Confirmation
            if (wMsg.Message.Subject == "Steam Trade Confirmation")
            {
                wMsg.Move("gaemz // Steam // Trade Confirmation", new List<string> { "gaemz", "Steam", "Trade Confirmation" });
                return;
            }

            // Wishlist item is on sale
            if (Regex.Match(wMsg.Message.Subject, @"(on|from) your Steam wishlist (is|are) (now )?on sale!$").Success ||
                Regex.Match(wMsg.Message.Subject, @"из вашего списка желаемого (в Steam\s+)?прода(е|ё|ю)тся со скидкой!$").Success)
            {
                wMsg.Move("gaemz // Steam // Wishlist item is on sale", new List<string> { "gaemz", "Steam", "Wishlist item is on sale" });
                return;
            }

            wMsg.Move("gaemz // Steam // Other", new List<string> { "gaemz", "Steam", "other" });
        }
    }
}
