// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces
{
    /// <summary>
    /// Defines a logger instances
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write a generic log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Write(string message, object? obj = null);

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Debug(string message, object? obj = null);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Information(string message, object? obj = null);

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Warning(string message, object? obj = null);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Error(string message, object? obj = null);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to be serialized into the log</param>
        public void Fatal(string message, object? obj = null);
    }
}
