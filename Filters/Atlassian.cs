using System.Collections.Generic;

namespace MailFilter.Filters
{
    internal class Atlassian
    {
        public static void Filter(WrappedMessage wMsg)
        {
            if (wMsg.Host.Contains("atlassian.net"))
            {
                Utils.MoveMessage("Jira", new List<string> { "Jira" }, wMsg);
                return;
            }

            if (wMsg.Host == "bitbucket.org")
            {
                Utils.MoveMessage("Bitbucket", new List<string> { "Bitbucket" }, wMsg);
            }
        }
    }
}
