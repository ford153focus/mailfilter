using System.Collections.Generic;

namespace MailFilter.Filters.Work
{
    public class Demis
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "a.shamsudinov@demis.ru")
            {
                if (wMsg.Message.Subject=="приход") wMsg.Delete();
                if (wMsg.Message.Subject=="уход") wMsg.Delete();
            }

            if (wMsg.SenderAddress == "reports@demis.ru")
            {
                if (wMsg.Message.Subject=="Турбо: открытые задачи")
                    wMsg.Move("reports :: open tasks", new List<string> {"reports", "open tasks"});
                if (wMsg.Message.Subject=="Ошибки списания времени")
                    wMsg.Move("reports :: time track error", new List<string> {"reports", "time track error"});
            }

            if (wMsg.SenderAddress == "portal@demis.ru")
            {
                if (wMsg.Message.Subject=="Живая лента Demis Group: что нового?")
                    wMsg.Move("portal :: news", new List<string> {"portal", "news"});
            }
        }
    }
}
