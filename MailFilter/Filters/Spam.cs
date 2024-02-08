using System.Linq;

namespace MailFilter.Filters;

internal class Spam
{
    public static void Filter(WrappedMessage wMsg)
    {
        if (wMsg.Message.To.Count > 3) {
            if (wMsg.Message.Attachments.Any()) {
                var attachment = wMsg.Message.Attachments.First();
                var a_fn = ((MimeKit.MimePart)attachment).FileName;
                if (a_fn.EndsWith(".pdf")) {
                    wMsg.Delete();
                }
            }
        }
    }
}