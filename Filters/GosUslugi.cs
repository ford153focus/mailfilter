using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class GosUslugi
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress.EndsWith("gosuslugi.ru") ||
                wMsg.SenderAddress.EndsWith("service-nalog.ru"))
            {
                wMsg.Move("GosUslugi", new List<string> { "ГосУслуги" });
            }
        }
    }
}
