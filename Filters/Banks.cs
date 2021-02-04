using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Banks
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host.Contains("bspb.ru"))
            {
                wMsg.Move("Banks // Bank SPB", new List<string> { "Banks", "Bank SPB" });
            }
            else if (wMsg.Host.Contains("raiffeisen.ru"))
            {
                wMsg.Move("Banks // Райффайзенбанк", new List<string> { "Banks", "Райффайзенбанк" });
            }

            else if (wMsg.Host.Contains("tinkoff.ru"))
            {
                if (wMsg.Message.Subject.Contains("Выписка по дебетовой карте"))
                {
                    wMsg.Move("Banks // Tinkoff // Выписка", new List<string> { "Banks", "Tinkoff", "Выписка" });
                }
                else
                {
                    wMsg.Move("Banks // Tinkoff", new List<string> { "Banks", "Tinkoff" });
                }
            }
        }
    }
}
