using System.Collections.Generic;

namespace MailFilter.Filters.Work;

internal class Banxe
{
    public static void Filter(WrappedMessage wMsg)
    {
        if (wMsg.SenderAddress == "gitlab@banxe.sits.pro") 
        {
            wMsg.Move("Work :: Banxe :: GitLab", ["gitlab"]);
            return;
        }

        if (wMsg.SenderAddress == "passbolt@banxe.sits.pro") 
        {
            wMsg.Move("Work :: Banxe :: PassBolt", ["passbolt"]);
            return;
        }

        if (wMsg.SenderAddress == "info@banxe.com") 
        {
            wMsg.Move("Work :: Banxe :: Info", ["news"]);
            return;
        }
    }
}