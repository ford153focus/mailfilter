using System;
using System.Collections.Generic;
using MailKit;
using MailKit.Net.Imap;

namespace MailFilter
{
    internal class Utils
    {
        public static void SuccessWrite(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WarningWrite(string message)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void MoveMessage(string outputMessage, List<string> targetFolder, ImapClient client,
            IMailFolder inbox, int index)
        {
            // Try create subfolder
            // Get subfolder on error
            IMailFolder currentFolder = client.GetFolder(client.PersonalNamespaces[0]);
            foreach (var folder in targetFolder)
                try
                {
                    currentFolder = currentFolder.GetSubfolder(folder);
                }
                catch (Exception ex)
                {
                    currentFolder = currentFolder.Create(folder, true);
                }

            // Move
            inbox.MoveTo(index, currentFolder);
            inbox.Expunge();

            SuccessWrite(outputMessage);
        }
    }
}