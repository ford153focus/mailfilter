using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MailFilter.Filters;

internal class Atlassian
{
    public static void Jira(WrappedMessage wMsg) {
        if (Regex.Match(wMsg.Subject, @"^\[JIRA\] .+ assigned .+ to you").Success)
        {
            wMsg.Move("jira :: assign", ["jira", "assigned_to_you"]);
            return;
        }

        if (Regex.Match(wMsg.Subject, @"^\[JIRA\] .+ mentioned you on").Success)
        {
            wMsg.Move("jira :: mention", ["jira", "mentioned_you"]);
            return;
        }

        if (Regex.Match(wMsg.Subject, @"^\[JIRA\] \([A-Z]+\-\d+\)").Success)
        {
            wMsg.Move("jira :: task edit", ["jira", "task_edit"]);
            return;
        }
    }

    public static void Filter(WrappedMessage wMsg)
    {
        if (Regex.Match(wMsg.SenderAddress, @"jira@.+\.atlassian\.net").Success)
        {
            Jira(wMsg);
            return;
        }

        if (Regex.Match(wMsg.SenderAddress, @"confluence@.+\.atlassian\.net").Success) 
        {
            wMsg.Move("Confluence", ["jira", "confluence"]);
            return;
        }

        if (wMsg.Host == "bitbucket.org")
        {
            wMsg.Move("BitBucket", ["bitbucket"]);
        }
    }
}