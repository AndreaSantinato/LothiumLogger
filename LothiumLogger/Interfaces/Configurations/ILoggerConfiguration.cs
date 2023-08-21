// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces.Configurations
{
    public interface ILoggerConfiguration
    {
        /// <summary>
        /// Add a new console sink rule to the logger's configuration
        /// </summary>
        /// <param name="minimumLogLevel">Defines the minimun accepted logging level</param>
        /// <param name="restrictedToLogLevel">Defines the only accepted logging level</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddConsoleSink
        (
            LogLevel minimumLogLevel = LogLevel.Normal,
            LogLevel restrictedToLogLevel = LogLevel.Normal
        );

        /// <summary>
        /// Add a new file sink rule to the logger's configuration
        /// </summary>
        /// <param name="name">Contains the name of the file</param>
        /// <param name="path">Contains the path of the file</param>
        /// <param name="minimumLogLevel">Defines the minimun accepted logging level</param>
        /// <param name="restrictedToLogLevel">Defines the only accepted logging level</param>
        /// <returns>A Logger Configuration Object</returns>
        public ILoggerConfiguration AddFileSink
        (
            string name = "",
            string path = "",
            LogLevel minimumLogLevel = LogLevel.Normal,
            LogLevel restrictedToLogLevel = LogLevel.Normal
        );

        public Logger Build();
    }
}

