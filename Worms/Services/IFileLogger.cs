using System;

namespace Worms.Services
{
    public interface IFileLogger
    {
        void LogInfo(string logMessage, params object[] args);

        void LogInfo(Exception exception, string logMessage, params object[] args);
    }
}