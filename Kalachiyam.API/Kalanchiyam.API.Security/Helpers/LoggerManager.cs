using NLog;
using System;


namespace Kalanchiyam.API.Security.Helpers
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private async void Log(LogLevel logLevel, string module, string shortMessage, string detailedMessage)
        {
             LogEventInfo theEvent = new LogEventInfo(logLevel, "Kalanchiyam NLog", shortMessage);
            theEvent.Properties["Module"] = module;
            theEvent.Properties["DetailedMessage"] = detailedMessage;
            logger.Log(theEvent);
        }

        public void Log(LogLevel logLevel, string module, string shortMessage)
        {
            Log(logLevel, module, shortMessage, string.Empty);
        }
        public void Log(LogLevel logLevel, string module, string shortMessage, params object[] args)
        {
            string message = string.Format(shortMessage, args);
            Log(logLevel, module, message, string.Empty);
        }

        public void LogException(string module, Exception ex, string shortMessage, params object[] args)
        {
            string message = string.Format(shortMessage, args);
            string detailedMessage = ex.ToString();
            Log(LogLevel.Fatal, module, message, detailedMessage);


        }



    }
}
