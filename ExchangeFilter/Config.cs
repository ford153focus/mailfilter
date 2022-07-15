using System.Text;
using Newtonsoft.Json;

namespace ExchangeFilter;

public class ConfigModel
{
    public string? exchangeVersion { get; set; }
    public string? uri { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }
    public string? domain { get; set; }
}

public class Config
{
    private static ConfigModel? _config;

    public static string ExchangeVersion
    {
        get
        {
            if (_config == null) ParseJsonFile();
            return _config.exchangeVersion;
        }
    }

    public static string Uri
    {
        get
        {
            if (_config == null) ParseJsonFile();
            return _config.uri;
        }
    }

    public static string Username
    {
        get
        {
            if (_config == null) ParseJsonFile();
            return _config.username;
        }
    }

    public static string Password
    {
        get
        {
            if (_config == null) ParseJsonFile();
            return _config.password;
        }
    }

    public static string Domain
    {
        get
        {
            if (_config == null) ParseJsonFile();
            return _config.domain;
        }
    }

    static void ParseJsonFile()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "config.json");
        var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        _config = JsonConvert.DeserializeObject<ConfigModel>(fileContent);
    }
}