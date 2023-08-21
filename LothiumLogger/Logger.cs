// System Class
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;
using LothiumLogger.Formatters;

namespace LothiumLogger
{
    /// <summary>
    /// Logger Class
    /// </summary>
    public sealed class Logger : ILogger, IDisposable
    {
        #region Class Property

        /// <summary>
        /// Used to detect redundant calls
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Contains the current configuration for the logger's instance
        /// </summary>
        private readonly LoggerConfiguration _config;

        #endregion

        #region Class Constructor & Destructor

        /// <summary>
        /// Logger Instance Constructor
        /// </summary>
        /// <param name="configuration">Contains the logger configuration used to create this instance</param>
        public Logger(LoggerConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Dispose the Logger Instance Previusly Created
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

        #region Core Private Methods

        /// <summary>
        /// Manage the writing of a new log based on the logger configuration and its writing settings
        /// </summary>
        /// <param name="loggerConfig">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event to write</param>
        /// <param name="obj">Contains an optional object to be serialized</param>
        private void WriteNewLog(
            LoggerConfiguration loggerConfig,
            LogEvent logEvent,
            object obj
        )
        {
            if (loggerConfig == null) return;
            if (loggerConfig.ConsoleSinkRules == null && (loggerConfig.FileSinkRules == null || loggerConfig.FileSinkRules.Count() == 0))
            {
                return;
            }
            else if (loggerConfig.ConsoleSinkRules == null)
            {
                return;
            }
            if (logEvent == null) return;

            // If the object is not null it will format the message with the serialized object result
            if (obj != null)
            {
                if (obj.GetType() == typeof(Exception))
                {
                    logEvent = JsonFormatter.FormatException((Exception)obj, logEvent);
                }
                else
                {
                    logEvent = JsonFormatter.FormatObject(obj, logEvent);
                }
            }

            // Console Sink
            if (loggerConfig.ConsoleSinkRules != null)
            {
                var minimumLevel = loggerConfig.ConsoleSinkRules.MinimumLevel;
                var restrictToLevel = loggerConfig.ConsoleSinkRules.RestrictedToLevel;

                if (restrictToLevel != LogLevel.Normal && logEvent.Level != restrictToLevel) return;

                if (!loggerConfig.ConsoleSinkRules.Initialize(loggerConfig, logEvent))
                {
                    loggerConfig.ConsoleSinkRules.Write(logEvent);
                }
            }

            // File Sink
            foreach (var fileSink in loggerConfig.FileSinkRules)
            {
                if (fileSink != null)
                {
                    var minimumLevel = loggerConfig.ConsoleSinkRules.MinimumLevel;
                    var restrictToLevel = loggerConfig.ConsoleSinkRules.RestrictedToLevel;

                    if (restrictToLevel != LogLevel.Normal && logEvent.Level != restrictToLevel) return;

                    if (!fileSink.Initialize(loggerConfig, logEvent))
                    {
                        fileSink.Write(logEvent);
                    }
                }
            }
        }

        /// <summary>
        /// Manage the writing of a new log based on an occured exception
        /// </summary>
        /// <param name="loggerConfig">Contains the current logger configuration</param>
        /// <param name="ex">Contains the occured exception</param>
        /// <param name="message">Contains a message</param>
        private void WriteNewLogFromException(
            LoggerConfiguration loggerConfig,
            Exception ex,
            LogLevel level,
            string message
        )
        {
            if (ex == null) return;
            if (String.IsNullOrEmpty(message))
            {
                message = "An error occured during the code execution:";
            }
            WriteNewLog(loggerConfig, new LogEvent(DateTime.Now, level, message), ex);
        }

        #endregion

        #region Default Log Methods

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Write(string message, object? obj = null)
                => Write(new LogEvent(DateTime.Now, LogLevel.Normal, message), obj);

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Write(LogEvent logEvent, object? obj = null)
            => WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Debug Log Methods

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Debug(string message, object? obj = null)
            => Debug(new LogEvent(DateTime.Now, LogLevel.Debug, message), obj);

        /// <summary>
        /// Write a normal log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Debug(LogEvent logEvent, object? obj = null)
            => WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Information Log Methods

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Information(string message, object? obj = null)
            => Information(new LogEvent(DateTime.Now, LogLevel.Info, message), obj);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Information(LogEvent logEvent, object? obj = null)
            => WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Warning Log Methods

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Warning(string message, object? obj = null)
            => Warning(new LogEvent(DateTime.Now, LogLevel.Warn, message), obj);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Warning(LogEvent logEvent, object? obj = null)
            => WriteNewLog(_config, logEvent, obj);

        #endregion

        #region Error Log Methods

        /// <summary>
        /// Write an error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Error(string message, object? obj = null)
            => Error(new LogEvent(DateTime.Now, LogLevel.Err, message), obj);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Error(LogEvent logEvent, object? obj = null)
        {
            if (obj != null && obj.GetType() == typeof(Exception))
            {
                WriteNewLogFromException(_config, (Exception)obj, LogLevel.Err, logEvent.Message);
            }
            else
            {
                WriteNewLog(_config, logEvent, obj);
            }
        }

        /// <summary>
        /// Write an error log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        public void Error(Exception exception)
            => Fatal(exception, new LogEvent(DateTime.Now, LogLevel.Err, "Exception Occured"));

        /// <summary>
        /// Write an error log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        /// <param name="logEvent">Contains the log event</param>
        public void Error(Exception exception, LogEvent logEvent)
            => Error(logEvent, exception);

        #endregion

        #region Fatal Log Methods

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Fatal(string message, object? obj = null)
            => Fatal(new LogEvent(DateTime.Now, LogLevel.Fatal, message), obj);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the optional object to serialize into the log</param>
        public void Fatal(LogEvent logEvent, object? obj = null)
        {
            if (obj != null && obj.GetType() == typeof(Exception))
            {
                WriteNewLogFromException(_config, (Exception)obj, LogLevel.Fatal, logEvent.Message);
            }
            else
            {
                WriteNewLog(_config, logEvent, obj);
            }
        }

        /// <summary>
        /// Write a fatal log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        public void Fatal(Exception exception)
            => Fatal(exception, new LogEvent(DateTime.Now, LogLevel.Fatal, "Fatal Exception Occured"));

        /// <summary>
        /// Write an fatal log from an exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        /// <param name="logEvent">Contains the log event</param>
        public void Fatal(Exception exception, LogEvent logEvent)
            => Fatal(logEvent, exception);

        #endregion
    }
}