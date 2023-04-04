using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Banks
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (true)
            {
                case true when wMsg.Host.EndsWith("bspb.ru"):
                    wMsg.Move("Banks // Bank SPB", new List<string> { "Banks", "Bank SPB" });
                    return;
                case true when wMsg.Host.EndsWith("paypal.com"):
                    wMsg.Move("Banks // PayPal", new List<string> { "Banks", "PayPal" });
                    return;
                case true when wMsg.Host.EndsWith("raiffeisen.ru"):
                    wMsg.Move("Banks // Райффайзенбанк", new List<string> { "Banks", "Райффайзенбанк" });
                    return;
                case true when wMsg.Host.EndsWith("tinkoff.ru"):
                    if (wMsg.Message.Subject.Contains("Выписка по дебетовой карте"))
                    {
                        wMsg.Move("Banks // Tinkoff // Выписка", new List<string> { "Banks", "Tinkoff", "Выписка" });
                    }
                    wMsg.Move("Banks // Tinkoff", new List<string> { "Banks", "Tinkoff" });
                    return;
            }
        }
    }
}
