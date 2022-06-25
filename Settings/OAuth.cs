using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MailFilter.Settings;

class OAuth
{
    public class Model
    {
        public class Installed
        {
            public string client_id { get; set; }
            public string project_id { get; set; }
            public string auth_uri { get; set; }
            public string token_uri { get; set; }
            public string auth_provider_x509_cert_url { get; set; }
            public string client_secret { get; set; }
            public List<string> redirect_uris { get; set; }
        }

        public class Root
        {
            public Installed installed { get; set; }
        }
    }

    public static Model.Root parsed;

    public static void ParseJsonFile()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "oauth_credentials.json");
        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        parsed = JsonConvert.DeserializeObject<Model.Root>(fileContent);
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
}
