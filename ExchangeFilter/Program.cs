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

        await Utils.Auth();
        var mails = await Utils.GetInboxMails();

        foreach (var email in mails)
        {
            await Pskb.Maintenance(email);
            await Pskb.Other(email);
            await Pskb.RedMine(email);
            await Pskb.ServiceDesk(email);
        }

        Console.WriteLine("Hello World!");
    }
}