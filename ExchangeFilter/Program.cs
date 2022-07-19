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
                await Pskb.Interposer(email);
                await Pskb.Maintenance(email);
                await Pskb.Other(email);
                await Pskb.RedMine(email);
                await Pskb.ServiceDesk(email);
                await Pskb.Skype(email);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        Console.WriteLine("Hello World!");
    }
}