using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Temporary
    {
        public static void Filter(WrappedMessage wMsg)
        {
            switch (wMsg.SenderAddress)
            {
                case "portal@azbukavkusa.ru":
                    wMsg.Move("Tmp", new List<string> { "tmp" });
                    break;
            }
        }
    }
}
