// System Class
using System;
// Core Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;
using LothiumLogger.Sinkers;

namespace LothiumLogger
{
    /// <summary>
    /// Configuration object used to create a new Logger instance
    /// </summary>
    public sealed class LoggerConfiguration : ILoggerConfiguration
    {
        #region Class property

        /// <summary>
        /// Contains all the sinking rules
        /// </summary>
        internal List<ISinker?> SinkRules { get; set; }

        #endregion

        #region Constructor & Destructor

        /// <summary>
        /// Create a new configuration for the logger class
        /// </summary>
        public LoggerConfiguration()
        {
            SinkRules = new List<ISinker?> { };
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Add a new console sink rule to the logger's configuration
        /// </summary>
        /// <param name="minimumLogLevel">Defines the minimun accepted logging level</param>
        /// <param name="restrictedToLogLevel">Defines the only accepted logging level</param>
        /// <param name="theme">Defines the console theme used</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddConsoleSinker
        (
            LogLevelEnum minimumLogLevel = LogLevelEnum.Normal,
            LogLevelEnum restrictedToLogLevel = LogLevelEnum.Normal,
            ConsoleThemeEnum theme = ConsoleThemeEnum.None
        )
        {
            // Add the console's sink to the SinkServices and return the updated configuration's object
            SinkRules.Add(
                new ConsoleSinker(
                    true, 
                    minimumLogLevel,
                    restrictedToLogLevel,
                    theme
                )
            );
            return this;
        }

        /// <summary>
        /// Add a new file sink rule to the logger's configuration
        /// </summary>
        /// <param name="name">Contains the name of the file</param>
        /// <param name="path">Contains the path of the file</param>
        /// <param name="minimumLogLevel">Defines the minimun accepted logging level</param>
        /// <param name="restrictedToLogLevel">Defines the only accepted logging level</param>
        /// <param name="typeOfGeneratedFile">Defines the type of the generated log files</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddFileSinker
        (
            string name = "",
            string path = "",
            LogLevelEnum minimumLogLevel = LogLevelEnum.Normal,
            LogLevelEnum restrictedToLogLevel = LogLevelEnum.Normal,
            LogFileTypeEnum typeOfGeneratedFile = LogFileTypeEnum.GenericLog
        )
        {
            // Add the file's sink to the SinkServices and return the updated configuration's object
            SinkRules.Add(
                new FileSinker(
                    true,
                    name,
                    path,
                    minimumLogLevel,
                    restrictedToLogLevel,
                    typeOfGeneratedFile
                )
            );       
            return this;
        }

        /// <summary>
        /// Add a custom sinker rule to the logger's configuration
        /// </summary>
        /// <param name="sinker">Contains the custom created sinker</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddCustomSinker(ISinker sinker)
        {
            if (sinker != null) SinkRules.Add(sinker);
            return this;
        }

        /// <summary>
        /// Build a new Logger from the current configuration
        /// </summary>
        /// <returns>A new logger object generated from the current configuration</returns>
        ILogger ILoggerConfiguration.Build()
        {
            // Check if the current configuration if correctly configured to generate a fully functional logger instance
            if (SinkRules == null) return null;

            // Return a new logger instance
            return new Logger(this);
        }

        #endregion
    }
}
