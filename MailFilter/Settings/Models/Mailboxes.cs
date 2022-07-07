using System.Collections.Generic;

namespace MailFilter.Settings.Models;

public class Mailbox
{
    public string login { get; set; }
    public string password { get; set; }
    public string host { get; set; }
    public int port { get; set; }
    public List<string> applicable_filters { get; set; }
}

public class Mailboxes
{
    public List<Mailbox> mailboxes { get; set; }
}
