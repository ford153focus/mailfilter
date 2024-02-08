using System.Collections.Generic;

namespace MailFilter.Filters;

internal class Kufar
{
    public static void Filter(WrappedMessage wMsg)
    {

        if (wMsg.Host == "kontakt.kufar.by")
        {
            wMsg.Move("kufar // msg", new List<string> { "kufar", "msg" });
            return;
        }

        if (wMsg.Host == "kufar.by")
        {
            if (wMsg.Subject == "у вас новое сообщение от Куфара")
            {
                wMsg.Move("kufar // msg", new List<string> { "kufar", "msg" });
                return;
            }

            wMsg.Move("kufar", new List<string> { "kufar" });
        }
    }
}
