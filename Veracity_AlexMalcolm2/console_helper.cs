using System;

namespace Veracity_AlexMalcolm
{
    public enum LogType
    {
        ERROR,
        INFO,
        CONSOLE_CONSUMER,
        DEBUG,
        PLAIN
    }
    public class ConsoleHelper
    {
        // Public functions
        public static void WriteLine(LogType type, string message = " ")
        {
            string messagePrepend = "";
            switch (type)
            {
                case (LogType.ERROR):
                    messagePrepend = "ERROR: ";
                    break;

                case (LogType.INFO):
                    messagePrepend = "INFO: ";
                    break;

                case (LogType.CONSOLE_CONSUMER):
                    messagePrepend = "CONSOLE CONSUMER:\t";
                    break;

                case (LogType.DEBUG):
                    messagePrepend = "DEBUG: ";
                    break;
            }

            if ((type == LogType.DEBUG && Program.DEBUG_ENABLED) || type != LogType.DEBUG)
                Console.WriteLine(string.Format("[{0}] {1} {2}", currentTimeFormatted(), messagePrepend, message));
        }
        public static string currentTimeFormatted()
        {
            return DateTime.Now.ToString("HH:mm:ss:ff");
        }
    }
}
