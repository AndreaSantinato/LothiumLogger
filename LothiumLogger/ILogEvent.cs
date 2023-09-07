// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces
{
    /// <summary>
    /// Interface that define a log event
    /// </summary>
    public interface ILogEvent
    {
        /// <summary>
        /// Indicates the Date of the log event
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Indicates the level of the log event
        /// </summary>
        public LogLevelEnum Level { get; set; }

        /// <summary>
        /// Contains the message of the log event
        /// </summary>
        public string? Message { get; set; }
    }
}
