using System;
using System.Collections.Generic;
using MailKit;

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

        public static void ErrorWrite(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void MoveMessage(string outputMessage, List<string> targetFolder, WrappedMessage wMsg)
        {
            try
            {
                // Get subfolder
                // Try to create subfolder on error
                IMailFolder currentFolder = wMsg.Client.GetFolder(wMsg.Client.PersonalNamespaces[0]);
                foreach (var folder in targetFolder)
                {
                    try
                    {
                        currentFolder = currentFolder.GetSubfolder(folder);
                    }
                    catch (Exception)
                    {
                        currentFolder = currentFolder.Create(folder, true);
                    }
                }

                // Move
                wMsg.Inbox.MoveTo(wMsg.Index, currentFolder);
                wMsg.Inbox.Expunge();

                SuccessWrite(outputMessage);
            }
            catch (Exception e)
            {
                ErrorWrite(e.ToString());
            }
        }
    }
}
