using Microsoft.Exchange.WebServices.Data;
using Task = System.Threading.Tasks.Task;

namespace ExchangeFilter;

public class Utils
{
    private static ExchangeService _service = null!;

    public static async Task Auth()
    {
        ExchangeVersion exchangeVersion;
        bool isValidEnum = Enum.TryParse(Config.ExchangeVersion, out exchangeVersion);
        _service = new ExchangeService(exchangeVersion);
        // service.TraceEnabled = true;
        // service.TraceFlags = TraceFlags.All;
        // await service.AutodiscoverUrl(Config.Username, RedirectionUrlValidationCallback);
        _service.Url = new Uri(Config.Uri);
        _service.Credentials = new WebCredentials(Config.Username, Config.Password, Config.Domain);
    }

    private static async Task<FolderId> GetFolder(List<string> target)
    {
        var pointer = await Folder.Bind(_service, WellKnownFolderName.MsgFolderRoot);
        await pointer.Load();
        var targetId = pointer.Id;

        foreach (var s in target)
        {
            var found = false; // flag - are we found a needle?

            // search needle
            foreach (var folder in await pointer.FindFolders(new FolderView(100)))
                if (folder.DisplayName == s)
                {
                    targetId = folder.Id;
                    found = true;
                    pointer = folder;
                }

            // if folder not found - create it
            if (!found)
            {
                var folder = new Folder(_service);
                folder.DisplayName = s;
                await folder.Save(pointer.Id);

                targetId = folder.Id;
                pointer = folder;
            }
        }

        return targetId;
    }

    public static async Task<List<EmailMessage>> GetInboxMails()
    {
        var offset = 0;
        var pageSize = 50;
        var more = true;
        var view = new ItemView(50, offset, OffsetBasePoint.Beginning);
        view.PropertySet = PropertySet.IdOnly;
        var emails = new List<EmailMessage>();
        while (more)
        {
            FindItemsResults<Item> findResults = await _service.FindItems(WellKnownFolderName.Inbox, view);
            foreach (var item in findResults.Items) emails.Add((EmailMessage)item);

            more = findResults.MoreAvailable;
            if (more) view.Offset += pageSize;
        }

        PropertySet
            properties =
                BasePropertySet.FirstClassProperties; //A PropertySet with the explicit properties you want goes here
        await _service.LoadPropertiesForItems(emails, properties);
        return emails;
    }

    public static async Task MoveMail(EmailMessage email, List<string> target)
    {
        var folderId = await GetFolder(target);
        var item = await email.Move(folderId);
        Console.WriteLine($"Message '{email.Subject}' moved to {folderId}");
    }

    public static async Task TestSend()
    {
        var email = new EmailMessage(_service);
        email.ToRecipients.Add("ford153focus@gmail.com");
        email.Subject = "HelloWorld";
        email.Body = new MessageBody("This is the first email I've sent by using the EWS Managed API.");
        await email.Send();
    }
}