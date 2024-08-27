using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MailFilter.Settings;

class Mailbox
{
    public static Models.Mailboxes parsed;

    public static void ParseJsonFile()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "cfg", "mailboxes.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Models.Mailboxes>(fileContent);
    }

    public static List<Models.Mailbox> GetAll()
    {
        if (parsed == null) ParseJsonFile();
        return parsed.mailboxes;
    }
}
