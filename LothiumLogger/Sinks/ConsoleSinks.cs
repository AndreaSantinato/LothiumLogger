// System Class
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces.Sinks;
using LothiumLogger.Formatters;

namespace LothiumLogger.Sinks
{
    /// <summary>
    /// Internal Class For Manage The Writing Of The Log Event Inside The Console
    /// </summary>
    internal class ConsoleSinks : IConsoleSinks
    {
        #region Dispose Safehandle Property

        /// <summary>
        /// Used to detect redundant calls
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion

        #region Class Property

        /// <summary>
        /// Define the type of the sink service
        /// </summary>
        internal Type SinkType => typeof(ConsoleSinks);

        /// <summary>
        /// Define if the logger can use this sink
        /// </summary>
        internal bool EnableLogging { get; set; }

        /// <summary>
        /// Indicates the minimum writing accepted level
        /// </summary>
        internal LogLevel MinimumLevel { get; set; }

        /// <summary>
        /// Indicates the only writing level accepted
        /// </summary>
        internal LogLevel RestrictedToLevel { get; set; }

        #endregion

        #region Class Constructor & Destructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="enableLogging">Define if the logger can write log inside the console</param>
        public ConsoleSinks
        (
            bool enableLogging,
            LogLevel minimumLogLevel = LogLevel.Normal,
            LogLevel restrictedToLogLevel = LogLevel.Normal
        )
        {
            EnableLogging = enableLogging;
            MinimumLevel = minimumLogLevel;
            RestrictedToLevel = restrictedToLogLevel;
        }

        /// <summary>
        /// Dispose the ConsoleLogger Instance
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

        #region Class Methods

        /// <summary>
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        public bool Initialize(
            LoggerConfiguration configuration,
            LogEvent logEvent
        )
        {
            if (!EnableLogging) return false;
            if (configuration == null) return false;
            if (logEvent == null) return false;
            if (string.IsNullOrEmpty(logEvent.Message)) return false;
            return logEvent.Level >= MinimumLevel ? false : true;
        }

        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        public void Write(LogEvent logEvent)
        {
            var message = LogFormatter.FormatLogMessage(logEvent, LogDateFormat.Standard);
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvents">Contains a list of log event occured</param>
        public void Write(List<LogEvent> logEvents)
        {
            if (logEvents != null && logEvents.Count() > 0)
            {
                foreach (var logEvent in logEvents)
                {
                    Write(logEvent);
                }
            }
        }

        #endregion
    }
}
