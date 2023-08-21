// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger
{
    /// <summary>
    /// Object that represent a log event
    /// </summary>
    public sealed class LogEvent
    {
        /// <summary>
        /// Indicates the Date of the log event
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Indicates the level of the log event
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Contains the message of the log event
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Class Constructor
        /// </summary>
        public LogEvent()
        {
            Date = DateTimeOffset.Now;
            Level = LogLevel.Normal;
            Message = string.Empty;
        }

        /// <summary>
        /// Class Extended Constructor
        /// </summary>
        /// <param name="date">Indicates the date when the log event occured</param>
        /// <param name="level">Indicates the level of the log event occured</param>
        /// <param name="message">Indicates the message of the log event occured</param>
        public LogEvent(DateTimeOffset date, LogLevel level, string? message)
        {
            Date = date;
            Level = level;
            Message = message;
        }
    }
}
