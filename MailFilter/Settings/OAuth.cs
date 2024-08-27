using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MailFilter.Settings;

class OAuth
{
    public static Models.OAuth parsed;

    public static void ParseJsonFile()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "oauth_credentials.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Models.OAuth>(fileContent);
    }

    public static string ClientId
    {
        get
        {
            if (parsed == null) ParseJsonFile();
            return parsed.installed.client_id;
        }
    }

    public static string ClientSecret
    {
        get
        {
            if (parsed == null) ParseJsonFile();
            return parsed.installed.client_secret;
        }
    }

    public static string GetClientId(string login)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "cfg", "gmail", login, "client_secret.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Models.OAuth>(fileContent);
        return parsed.installed.client_id;
    }

    public static string GetClientSecret(string login)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "cfg", "gmail", login, "client_secret.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Models.OAuth>(fileContent);
        return parsed.installed.client_secret;
    }
}
