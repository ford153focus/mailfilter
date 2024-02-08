using Renci.SshNet;

namespace MailFilter;

public static class Notifier
{
    private static void NotifyWindowsOverSsh(string address = "192.168.0.104", int port=22, string login="focus", string password="ford", string msg = "halp")
    {
        SshClient sshclient = new SshClient("192.168.0.104", "focus", "ford");
        sshclient.Connect();
        SshCommand sc = sshclient.CreateCommand("msg focus /v MailFilter lost gmail token");
        sc.Execute();
        //string answer = sc.Result;
    }
}