using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Gitlab
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host != "dev.indins.ru") return;

            if (wMsg.Message.Subject.StartsWith("Access to the ") && wMsg.Message.Subject.EndsWith(" project was granted"))
            {
                Utils.MoveMessage("Gitlab // Access granted", new List<string> { "Gitlab", "Access granted" }, wMsg);
                return;
            }
        }
    }
}
