using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Gaemz
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.host)
            {
                case "amplitude-studios.com":
                case "battlerite.com":
                case "crypticstudios.com":
                case "email.games2gether.com":
                case "gaijin.net":
                case "mobafire.com":
                case "playkey.net":
                    Utils.MoveMessage("gaemz // Other", new List<string> { "gaemz", "Other" }, wMsg);
                    return;
                case "ru.playblackdesert.com":
                    Utils.MoveMessage("gaemz // bdesert", new List<string> { "gaemz", "black_desert" }, wMsg);
                    return;
                case "blizzard.com":
                case "em.blizzard.com":
                    Utils.MoveMessage("gaemz // Blizzard", new List<string> { "gaemz", "Blizzard" }, wMsg);
                    return;
                case "capcom.com":
                    Utils.MoveMessage("gaemz // Capcom", new List<string> { "gaemz", "Capcom" }, wMsg);
                    return;
                case "discordapp.com":
                    Utils.MoveMessage("gaemz // Discord", new List<string> { "gaemz", "Discord" }, wMsg);
                    return;
                case "daybreakgames.com":
                case "email.daybreakgames.com":
                    Utils.MoveMessage("gaemz // H1Z1", new List<string> { "gaemz", "H1Z1" }, wMsg);
                    return;
                case "ea.com":
                case "e.ea.com":
                    Utils.MoveMessage("gaemz // EA", new List<string> { "gaemz", "EA" }, wMsg);
                    return;
                case "epicgames.com":
                    Utils.MoveMessage("gaemz // Epic Games", new List<string> { "gaemz", "Epic Games" }, wMsg);
                    return;
                case "gog.com":
                case "email.gog.com":
                case "email2.gog.com":
                    Utils.MoveMessage("gaemz // GOG.com", new List<string> { "gaemz", "GOG.com" }, wMsg);
                    return;
                case "goodgame.ru":
                    Utils.MoveMessage("gaemz // GoodGame", new List<string> { "gaemz", "GoodGame" }, wMsg);
                    return;
                case "hirezstudios.com":
                    Utils.MoveMessage("gaemz // Hi-Rez Studios", new List<string> { "gaemz", "Hi-Rez Studios" }, wMsg);
                    return;
                case "humblebundle.com":
                case "mailer.humblebundle.com":
                case "mg.humblebundle.com":
                    Utils.MoveMessage("gaemz // Humble Bundle", new List<string> { "gaemz", "Humble Bundle" }, wMsg);
                    return;
                case "perfectworld.com":
                    Utils.MoveMessage("gaemz // Perfect World", new List<string> { "gaemz", "Perfect World" }, wMsg);
                    return;
                case "ppy.sh":
                    Utils.MoveMessage("gaemz // OSU!", new List<string> { "gaemz", "osu" }, wMsg);
                    return;
                case "robotcache.com":
                    Utils.MoveMessage("gaemz // Robot Cache", new List<string> { "gaemz", "Robot Cache" }, wMsg);
                    return;
                case "rockstargames.com":
                    Utils.MoveMessage("gaemz // Rockstar Games", new List<string> { "gaemz", "Rockstar Games" }, wMsg);
                    return;
                case "stopgame.ru":
                    Utils.MoveMessage("gaemz // StopGame.ru", new List<string> { "gaemz", "StopGame.ru" }, wMsg);
                    return;
                case "twitch.tv":
                case "sfmarketing.twitch.tv":
                    Utils.MoveMessage("gaemz // Twitch", new List<string> { "gaemz", "Twitch" }, wMsg);
                    return;
                case "unity3d.com":
                    Utils.MoveMessage("gaemz // Unity 3D Engine", new List<string> { "gaemz", "Unity 3D Engine" }, wMsg);
                    return;
                case "warframe.com":
                    Utils.MoveMessage("gaemz // Warframe", new List<string> { "gaemz", "Warframe" }, wMsg);
                    return;
                case "wasd.tv":
                    Utils.MoveMessage("gaemz // WASD.TV", new List<string> { "gaemz", "WASD.TV" }, wMsg);
                    return;
            }

            if (wMsg.host.Contains("riotgames"))
            {
                Utils.MoveMessage("gaemz // LoL", new List<string> { "gaemz", "LoL" }, wMsg);
                return;
            }
            else if (wMsg.host.Contains("ubi"))
            {
                Utils.MoveMessage("gaemz // Ubisoft", new List<string> { "gaemz", "Ubisoft" }, wMsg);
                return;
            }
            else if (wMsg.host.Contains("warframe.com"))
            {
                Utils.MoveMessage("gaemz // Warframe", new List<string> { "gaemz", "Warframe" }, wMsg);
                return;
            }

            Steam(wMsg);
        }

        public static void Steam(WrappedMessage wMsg)
        {
            if (wMsg.host != "steampowered.com") return;

            // Community Market Purchase
            if (wMsg.message.Subject == "Thank you for your Community Market purchase")
            {
                Utils.MoveMessage("gaemz // Steam // Community Market Purchase", new List<string> { "gaemz", "Steam", "Community Market Purchase" }, wMsg);
                return;
            }

            // Gift received
            if (wMsg.message.Subject.Contains("You've received a gift copy of the game ") ||
                wMsg.message.Subject.Contains("Вы получили в подарок копию игры "))
            {
                Utils.MoveMessage("gaemz // Steam // Gift received", new List<string> { "gaemz", "Steam", "Gift received" }, wMsg);
                return;
            }

            // Item is sold on the Community Market
            if (wMsg.message.Subject == "You have sold an item on the Community Market" ||
                wMsg.message.Subject == "Вы продали предмет на Торговой площадке")
            {
                Utils.MoveMessage("gaemz // Steam // Item is sold on the Community Market", new List<string> { "gaemz", "Steam", "Item is sold on the Community Market" }, wMsg);
                return;
            }

            // Items Delivered
            if (wMsg.message.Subject == "Steam Trade Items Delivered")
            {
                Utils.MoveMessage("gaemz // Steam // Trade Items Delivered", new List<string> { "gaemz", "Steam", "Trade Items Delivered" }, wMsg);
                return;
            }

            // Released
            if (wMsg.message.Subject.Contains(" is now available on Steam!") ||
                wMsg.message.Subject.Contains(" is now available in Early Access on Steam!") ||
                wMsg.message.Subject.Contains(" уже доступна в Steam!"))
            {
                Utils.MoveMessage("gaemz // Steam // Released", new List<string> { "gaemz", "Steam", "Released" }, wMsg);
                return;
            }

            // Support
            if (wMsg.senderName == "Steam Support")
            {
                Utils.MoveMessage("gaemz // Steam // Support", new List<string> { "gaemz", "Steam", "Support" }, wMsg);
                return;
            }

            // Trade Confirmation
            if (wMsg.message.Subject == "Steam Trade Confirmation")
            {
                Utils.MoveMessage("gaemz // Steam // Trade Confirmation", new List<string> { "gaemz", "Steam", "Trade Confirmation" }, wMsg);
                return;
            }

            // Wishlist item is on sale
            if (wMsg.message.Subject == "An item on your Steam wishlist is on sale!" ||
                wMsg.message.Subject == "Товар из вашего списка желаемого продается со скидкой!" ||
                wMsg.message.Subject.Contains(" from your Steam wishlist is now on sale!") ||
                wMsg.message.Subject.Contains(" from your Steam wishlist are on sale!") ||
                wMsg.message.Subject.Contains(" items from your Steam wishlist are now on sale!") ||
                wMsg.message.Subject.Contains(" из вашего списка желаемого в Steam  продаётся со скидкой!") ||
                wMsg.message.Subject.Contains(" из вашего списка желаемого продаются со скидкой!"))
            {
                Utils.MoveMessage("gaemz // Steam // Wishlist item is on sale", new List<string> { "gaemz", "Steam", "Wishlist item is on sale" }, wMsg);
                return;
            }
        }
    }
}
