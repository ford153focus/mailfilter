using System.Collections.Generic;

namespace MailFilter.Filters.Work
{
    public class Pskb
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress == "shamsudinov_ao@pskb.com")
            {
                wMsg.Move("wrk :: pskb :: interposer", new List<string> {"wrk", "pskb", "interposer"});
            }

            if (wMsg.Host.EndsWith("pskb.com"))
            {
                wMsg.Move("wrk :: pskb", new List<string> {"wrk", "pskb"});
            }
        }
    }
}
