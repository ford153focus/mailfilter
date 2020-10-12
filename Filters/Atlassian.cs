using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Atlassian
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.host.Contains("atlassian.net"))
            {
                Utils.MoveMessage("Jira", new List<string> { "Jira" }, wMsg);
                return;
            }

            if (wMsg.host == "bitbucket.org")
            {
                Utils.MoveMessage("Bitbucket", new List<string> { "Bitbucket" }, wMsg);
            }
        }
    }
}
