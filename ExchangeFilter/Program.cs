using System.Net;
using ExchangeFilter.Filters;
using Microsoft.Exchange.WebServices.Data;
using Task = System.Threading.Tasks.Task;

namespace ExchangeFilter;

internal class Program
{
    private static async Task Main()
    {
        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        Utils.Auth();
        var mails = await Utils.GetInboxMails();

        foreach (var email in mails)
        {
            Console.WriteLine(email.Subject);
            try
            {
                var mail = new WrappedMessage(email);
                
                await Pskb.Interposer(mail);
                await Pskb.Maintenance(mail);
                await Pskb.Other(mail);
                await Pskb.RedMine(mail);
                await Pskb.ServiceDesk(mail);
                await Pskb.Skype(mail);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        Console.WriteLine("Hello World!");
    }
}