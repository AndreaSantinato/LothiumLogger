// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger;
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces.Configurations;
using LothiumLogger.Sinks;

namespace LothiumLogger
{
    /// <summary>
    /// Configuration object used to create a new Logger instance
    /// </summary>
    public sealed class LoggerConfiguration : ILoggerConfiguration
    {
        #region Class property

        /// <summary>
        /// Contains the rules for the console's sink
        /// </summary>
        internal ConsoleSinks? ConsoleSinkRules { get; set; }

        /// <summary>
        /// Contains a list of rules for the file's sink
        /// </summary>
        internal List<FileSinks?> FileSinkRules { get; set; }

        #endregion

        #region Constructor & Destructor

        /// <summary>
        /// Create a new configuration for the logger class
        /// </summary>
        public LoggerConfiguration()
        {
            ConsoleSinkRules = null;
            FileSinkRules = new List<FileSinks?> { };
        }

        #endregion

        #region Class Methods

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
        )
        {
            // Retrive the current configuration
            var configuration = this;

            // Add the console's sink to the SinkServices and return the current configuration
            if (ConsoleSinkRules == null) 
            {
                ConsoleSinkRules = new ConsoleSinks(true, minimumLogLevel, restrictedToLogLevel);
            }
            return configuration;
        }

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
        )
        {
            // Retrive the current configuration
            var configuration = this;

            // Add the file's sink to the SinkServices and return the current configuration
            if (FileSinkRules == null)
            {
                FileSinkRules = new List<FileSinks?> ();
            }
            FileSinkRules.Add(new FileSinks(true, name, path, minimumLogLevel, restrictedToLogLevel));
            return configuration;
        }

        /// <summary>
        /// Build a new Logger from the current configuration
        /// </summary>
        /// <returns>A new logger object generated from the current configuration</returns>
        public Logger Build()
        {
            // Retrive the current configuration
            var configuration = this;

            // Check if the current configuration if correctly configured to generate a fully functional logger instance
            if (configuration == null)
            {
                return null;
            }
            if (ConsoleSinkRules == null)
            {
                return null;
            }

            // Return a new logger instance
            return new Logger(configuration);
        }

        #endregion
    }
}
