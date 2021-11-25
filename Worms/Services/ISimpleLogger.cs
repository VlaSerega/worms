using System;

namespace Worms.Services
{
    public interface ISimpleLogger
    {
        void LogInfo(string logMessage, params object[] args);

        void LogInfo(Exception exception, string logMessage, params object[] args);
    }
}