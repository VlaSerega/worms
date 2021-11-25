using System;

namespace Worms.Services
{
    public class SimpleConsoleLogger : ISimpleLogger
    {
        public SimpleConsoleLogger()
        {
        }

        public void LogInfo(string logMessage, params object[] args)
        {
            string message = String.Format(logMessage, args);
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            Console.WriteLine("  :");
            Console.WriteLine(message);
            Console.WriteLine("-------------------------------");
        }

        public void LogInfo(Exception exception, string logMessage, params object[] args)
        {
            string message = String.Format(logMessage, args);
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            Console.WriteLine("  :");
            Console.WriteLine(exception);
            Console.WriteLine("     " + message);
            Console.WriteLine("-------------------------------");
        }
    }
}