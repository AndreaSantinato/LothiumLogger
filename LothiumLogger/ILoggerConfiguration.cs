// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Sinkers;

namespace LothiumLogger.Interfaces
{
    /// <summary>
    /// Defines a logger's configuration
    /// </summary>
    public interface ILoggerConfiguration
    {
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
        );

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
        );

        /// <summary>
        /// Add a custom sinker rule to the logger's configuration
        /// </summary>
        /// <param name="sinker">Contains the custom created sinker</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddCustomSinker(ISinker sinker);

        /// <summary>
        /// Build a new Logger from the current configuration
        /// </summary>
        /// <returns>A new logger object generated from the current configuration</returns>
        public ILogger Build();
    }
}
