using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MailFilter.Settings;

class Mailbox
{
    public class Model
    {
        public class Mailbox
        {
            public string login { get; set; }
            public string password { get; set; }
            public string host { get; set; }
            public int port { get; set; }
            public List<string> applicable_filters { get; set; }
        }

        public class Root
        {
            public List<Mailbox> mailboxes { get; set; }
        }
    }

    public static Model.Root parsed;

    public static void ParseJsonFile()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "mailboxes.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Model.Root>(fileContent);
    }

    public static List<Model.Mailbox> GetAll()
    {
        if (parsed == null) ParseJsonFile();
        return parsed.mailboxes;
    }
}
