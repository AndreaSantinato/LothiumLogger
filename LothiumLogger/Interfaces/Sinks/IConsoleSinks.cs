// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces.Sinks
{
    internal interface IConsoleSinks : IDisposable
    {
        /// <summary>
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        bool Initialize(LoggerConfiguration configuration, LogEvent logEvent);

        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        void Write(LogEvent logEvent);

        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvents">Contains a list of log event occured</param>
        void Write(List<LogEvent> logEvents);
    }
}
