using System;
using System.IO;

namespace Worms.Services
{
    public class SimpleFileLogger : IFileLogger, IDisposable
    {
        private readonly StreamWriter _writer;
        private bool _disposed = false;

        public SimpleFileLogger()
        {
            _writer = File.AppendText("log.txt");
        }

        public void LogInfo(string logMessage, params object[] args)
        {
            string message = String.Format(logMessage, args);
            _writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            _writer.WriteLine("  :");
            _writer.WriteLine(message);
            _writer.WriteLine("-------------------------------");
            _writer.Flush();
        }

        public void LogInfo(Exception exception, string logMessage, params object[] args)
        {
            string message = String.Format(logMessage, args);
            _writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            _writer.WriteLine("  :");
            _writer.WriteLine(exception);
            _writer.WriteLine("     " + message);
            _writer.WriteLine("-------------------------------");
            _writer.Flush();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   _writer.Dispose();
                }
            }
        }

        ~SimpleFileLogger()
        {
            Dispose(false);
        }
    }
}