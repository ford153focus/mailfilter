using System.Collections.Generic;

namespace MailFilter.Filters.Work
{
    internal class Trilimes
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.SenderAddress != "jira@trilimes.atlassian.net") return;

            wMsg.Move("Work // Trilimes", new List<string> { "Work", "Trilimes" });

        }
    }
}
