using System;
using System.Text;

namespace MailFilter
{
    public static class ConsoleUtils
    {
        public static void WriteError(string message)
        {
            Console.OutputEncoding = Encoding.Default;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteSuccess(string message)
        {
            Console.OutputEncoding = Encoding.Default;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteWarning(string message)
        {
            Console.OutputEncoding = Encoding.Default;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(message);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
