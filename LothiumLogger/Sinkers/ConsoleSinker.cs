// System Class
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
// Core Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;
using LothiumLogger.Sinkers.Themes;
using LothiumLogger.Sinkers.Formatters;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Internal class for the writing of the log's events inside the console
    /// </summary>
    internal class ConsoleSinker : ISinker
    {
        #region Private Class Property

        private SinkTypeEnum _sinkType { get; set; }
        private bool _enableLogging { get; set; }
        private LogLevelEnum _minimumLevel { get; set; }
        private LogLevelEnum _restrictedToLevel { get; set; }
        private IConsoleSinkerThemes _consoleThemes { get; set; }

        #endregion

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

        #region Class Constructor & Destructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="enableLogging">Define if the logger can write log inside the console</param>
        /// <param name="minimumLogLevel">Define the minimum accepted logging level to write</param>
        /// <param name="restrictedToLogLevel">Define the only accepted logging level to write</param>
        /// <param name="themeName">Define the console theme that will be used</param>
        public ConsoleSinker
        (
            bool enableLogging,
            LogLevelEnum minimumLogLevel = LogLevelEnum.Normal,
            LogLevelEnum restrictedToLogLevel = LogLevelEnum.Normal,
            ConsoleThemeEnum themeName = ConsoleThemeEnum.None
        )
        {
            _sinkType = SinkTypeEnum.Console;
            _enableLogging = enableLogging;
            _minimumLevel = minimumLogLevel;
            _restrictedToLevel = restrictedToLogLevel;
            _consoleThemes = RetriveThemeFromName(themeName);
        }

        /// <summary>
        /// Dispose the ConsoleLogger Instance
        /// </summary>
        void IDisposable.Dispose()
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
        /// Retrive the theme from a passed name
        /// </summary>
        /// <param name="themeName">Contains the console theme's name</param>
        /// <returns>One specific console theme</returns>
        IConsoleSinkerThemes RetriveThemeFromName(ConsoleThemeEnum themeName)
        {
            IConsoleSinkerThemes? consoleTheme = null;
            
            switch (themeName)
            {
                case ConsoleThemeEnum.None:
                    // Do Nothing
                    break;
                case ConsoleThemeEnum.LothiumLight:
                    consoleTheme = new LothiumLight();
                    break;
                case ConsoleThemeEnum.LothiumDark:
                    consoleTheme = new LothiumDark();
                    break;
            }
            
            return consoleTheme;
        }

        /// <summary>
        /// Return the sink's type
        /// </summary>
        /// <returns>Sink's Type</returns>
        public SinkTypeEnum GetSinkType() => _sinkType;

        /// <summary>
        /// Define if the sink is enabled for logging events
        /// </summary>
        /// <returns>True Or False Status</returns>
        public bool IsSinkerEnabled() => _enableLogging;

        /// <summary>
        /// Return the minimum accepted log level accepted by the sink
        /// </summary>
        /// <returns>Minimum accepted log's level foreach log event</returns>
        public LogLevelEnum GetMinimumLogLevel() => _minimumLevel;

        /// <summary>
        /// Return the only accepted log level by the sink
        /// </summary>
        /// <returns>The only accepted log level foreach log event</returns>
        public LogLevelEnum GetRestrictedLogLevel() => _restrictedToLevel;

        /// <summary>
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        public bool Initialize(ILoggerConfiguration configuration, ILogEvent logEvent)
        {
            // Check if the sink can write the log
            if (!IsSinkerEnabled() || 
                configuration == null|| 
                logEvent == null ||
                string.IsNullOrEmpty(logEvent.Message)
            ) return false;

            // Check if the log's restricted level allow the sink to write the log     
            if (GetRestrictedLogLevel() != LogLevelEnum.Normal && GetRestrictedLogLevel() == logEvent.Level) return true;

            // Check if the log's minimum level allow the sink to write the log
            return logEvent.Level >= GetMinimumLogLevel() ? true : false;
        }

        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        public void Write(ILogEvent logEvent)
        {
            var message = LogFormatter.FormatLogMessage(logEvent, LogDateFormatEnum.Standard);
            if (!string.IsNullOrEmpty(message))
            {
                // Inizialize the theme values
                if (_consoleThemes != null)
                {
                    Console.BackgroundColor = _consoleThemes.GetConsoleBackgroudColor();
                    Console.ForegroundColor = _consoleThemes.GetTextForeColorByLogLevel(logEvent.Level);
                }

                // Write the log message inside the console
                Console.WriteLine(message);
            }
        }

        #endregion
    }
}
