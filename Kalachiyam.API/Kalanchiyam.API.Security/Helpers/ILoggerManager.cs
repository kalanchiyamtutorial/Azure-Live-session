
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalanchiyam.API.Security.Helpers
{
    public interface ILoggerManager
    {
     
        void Log(LogLevel logLevel, string module, string shortMessage, params object[] args);
        void LogException(string module, Exception ex, string shortMessage, params object[] args);


    }
}
