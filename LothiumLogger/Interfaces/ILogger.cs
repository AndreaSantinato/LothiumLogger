// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LothiumLogger.Interfaces
{
    internal interface ILogger
    {
        /// <summary>
        /// Write a generic log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Write(LogEvent logEvent, object? obj = null);

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Debug(LogEvent logEvent, object? obj = null);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Information(LogEvent logEvent, object? obj = null);

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Warning(LogEvent logEvent, object? obj = null);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Error(LogEvent logEvent, object? obj = null);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Fatal(LogEvent logEvent, object? obj = null);
    }
}
