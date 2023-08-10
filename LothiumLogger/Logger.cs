// System Class
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
// Custom Class
using LothiumLogger.Core;
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger
{
    /// <summary>
    /// Logger Class
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Contains the current configuration for the logger's istance
        /// </summary>
        private readonly LoggerConfiguration _config;

        #region Class Constructor

        public Logger(LoggerConfiguration configuration)
        {
            _config = configuration;
        }

        #endregion

        #region Default Log Methods

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Write(string message)
            => Write(message, null);

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Write(string message, object obj)
            => CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Normal, message), obj);

        #endregion

        #region Debug Log Methods

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Debug(string message)
            => Debug(message, null);

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Debug(string message, object obj)
            => CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Debug, message), obj);

        #endregion

        #region Information Log Methods

        /// <summary>
        /// Write an information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Information(string message)
            => Information(message, null);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Information(string message, object obj)
            => CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Info, message), obj);

        #endregion

        #region Warning Log Methods

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Warning(string message)
            => Warning(message, null);

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Warning(string message, object obj)
            => CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Warn, message), obj);

        #endregion

        #region Error Log Methods

        /// <summary>
        /// Write an error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Error(string message)
            => Error(message, null);

        /// <summary>
        /// Write an error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Error(string message, object obj)
        {
            if (obj != null && obj.GetType() == typeof(Exception)) 
            {
                CoreLogging.WriteNewLogFromException(_config, (Exception)obj, LogLevel.Err, message);
                return;
            }

            CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Err, message), obj);
        }

        /// <summary>
        /// Write an error log from an exception
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Error(Exception exception)
            => Error("Exception Occured:", exception);

        #endregion

        #region Fatal Log Methods

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Fatal(string message)
            => Fatal(message, null);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to serialize into the log</param>
        public void Fatal(string message, object obj)
        {
            if (obj != null && obj.GetType() == typeof(Exception))
            {
                CoreLogging.WriteNewLogFromException(_config, (Exception)obj, LogLevel.Fatal, message);
                return;
            }

            CoreLogging.WriteNewLog(_config, new LogEventObject(DateTime.Now, LogLevel.Fatal, message), obj);
        }

        /// <summary>
        /// Write a fatal log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        public void Fatal(Exception exception)
            => Fatal("Fatal Exception Occured:", exception);

        #endregion
    }
}