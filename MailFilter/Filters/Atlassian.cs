using System.Collections.Generic;

namespace MailFilter.Filters;

internal class Atlassian
{
    public static void Filter(WrappedMessage wMsg)
    {
        if (wMsg.Host.Contains("atlassian.net"))
        {
            wMsg.Move("Jira", new List<string> { "Jira" });
            return;
        }

        if (wMsg.Host == "bitbucket.org")
        {
            wMsg.Move("Bitbucket", new List<string> { "Bitbucket" });
        }
    }
}