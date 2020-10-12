using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Banks
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host.Contains("bspb.ru"))
            {
                Utils.MoveMessage("Banks // Bank SPB", new List<string> { "Banks", "Bank SPB" }, wMsg);
            }

            if (wMsg.Host.Contains("tinkoff.ru"))
            {
                Utils.MoveMessage("Banks // Tinkoff", new List<string> { "Banks", "Tinkoff" }, wMsg);
            }
        }
    }
}
