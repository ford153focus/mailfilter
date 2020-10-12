using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class GosUslugi
    {
        public static void Filter(WrappedMessage wMsg)
        {
            List<string> gosuMailboxes = new List<string> {
                "no-reply@gosuslugi.ru",
                "no_reply@fcod.nalog.ru"
            };

            if (gosuMailboxes.Contains(wMsg.SenderAddress))
                Utils.MoveMessage("GosUslugi", new List<string> { "ГосУслуги" }, wMsg);
        }
    }
}
