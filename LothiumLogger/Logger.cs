// System Class
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;
// Custom Class
using LothiumLogger.Core;
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger
{
    /// <summary>
    /// Logger Class
    /// </summary>
    public sealed class Logger : ILogger, IDisposable
    {
        /// <summary>
        /// Used to detect redundant calls
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Contains the current configuration for the logger's istance
        /// </summary>
        private readonly LoggerConfiguration _config;

        #region Class Constructor & Destructor

        /// <summary>
        /// Logger Istance Constructor
        /// </summary>
        /// <param name="configuration">Contains the logger configuration used to create this istance</param>
        public Logger(LoggerConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Dispose the Logger Istance Previusly Created
        /// </summary>
        public void Dispose()
        {
            if (!_disposedValue)
            {
                _safeHandle.Dispose();
                _disposedValue = true;
            }
        }

        #endregion

        #region Default Log Methods

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Write(string message, object? obj = null)
            => Write(new LogEventObject(DateTime.Now, LogLevel.Normal, message), obj);

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Write(LogEventObject logEvent, object? obj = null)
            => CoreLogging.WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Debug Log Methods

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Debug(string message, object? obj = null)
            => Debug(new LogEventObject(DateTime.Now, LogLevel.Debug, message), obj);

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Debug(LogEventObject logEvent, object? obj = null)
            => CoreLogging.WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Information Log Methods

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Information(string message, object? obj = null)
            => Information(new LogEventObject(DateTime.Now, LogLevel.Info, message), obj);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Information(LogEventObject logEvent, object? obj = null)
            => CoreLogging.WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Warning Log Methods

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Warning(string message, object? obj = null)
            => Warning(new LogEventObject(DateTime.Now, LogLevel.Warn, message), obj);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Warning(LogEventObject logEvent, object? obj = null)
            => CoreLogging.WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Error Log Methods

        /// <summary>
        /// Write an error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Error(string message, object? obj = null)
            => Error(new LogEventObject(DateTime.Now, LogLevel.Err, message), obj);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Error(LogEventObject logEvent, object? obj = null)
        {
            if (obj != null && obj.GetType() == typeof(Exception))
            {
                CoreLogging.WriteNewLogFromException(_config, (Exception)obj, LogLevel.Err, logEvent.EventMessage);
            }
            else
            {
                CoreLogging.WriteNewLog(_config, logEvent, obj);
            }
        }

        /// <summary>
        /// Write an error log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        public void Error(Exception exception)
            => Fatal(exception, new LogEventObject(DateTime.Now, LogLevel.Err, "Exception Occured"));

        /// <summary>
        /// Write an error log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        /// <param name="logEvent">Contains the log event</param>
        public void Error(Exception exception, LogEventObject logEvent)
            => Error(logEvent, exception);

        #endregion

        #region Fatal Log Methods

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Fatal(string message, object? obj = null)
            => Fatal(new LogEventObject(DateTime.Now, LogLevel.Fatal, message), obj);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Fatal(LogEventObject logEvent, object? obj = null)
        {
            if (obj != null && obj.GetType() == typeof(Exception))
            {
                CoreLogging.WriteNewLogFromException(_config, (Exception)obj, LogLevel.Fatal, logEvent.EventMessage);
            }
            else
            {
                CoreLogging.WriteNewLog(_config, logEvent, obj);
            }
        }

        /// <summary>
        /// Write a fatal log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        public void Fatal(Exception exception)
            => Fatal(exception, new LogEventObject(DateTime.Now, LogLevel.Fatal, "Fatal Exception Occured"));

        /// <summary>
        /// Write an fatal log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        /// <param name="logEvent">Contains the log event</param>
        public void Fatal(Exception exception, LogEventObject logEvent)
            => Fatal(logEvent, exception);

        #endregion
    }
}