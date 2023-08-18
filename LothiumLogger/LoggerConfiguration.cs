// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Configurations;
using LothiumLogger.Interfaces;

namespace LothiumLogger
{
    public class LoggerConfiguration : ILoggerConfiguration
    {
        #region Class property

        /// <summary>
        /// Defines if the logger will write event to the debug console
        /// </summary>
        internal protected bool EnableConsoleLogging { get; internal set; }

        /// <summary>
        /// Defines if the logger will write event to one or more files
        /// </summary>
        internal protected bool EnableFileLogging { get; internal set; }

        /// <summary>
        /// Defines the writing rules (Console & File)
        /// </summary>
        internal protected List<WriteLogToConfiguration>? WriteLogConfigurations { get; internal set; }

        #endregion

        #region Constructor & Destructor

        /// <summary>
        /// Create a new configuration for the logger class
        /// </summary>
        public LoggerConfiguration()
        {
            EnableConsoleLogging = true;
            EnableFileLogging = false;
            WriteLogConfigurations = new List<WriteLogToConfiguration>();
        }

        #endregion

        /// <summary>
        /// Add a new write to console settings to the current configuration
        /// </summary>
        /// <param name="minimumLevel">Indicates the minimum logging level that will be logged to the console</param>
        /// <param name="restrictedToLevel">Indicates the restricted level that will be logged to the console, this will override the other parameter</param>
        /// <returns></returns>
        public LoggerConfiguration WriteToConsole(
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        )
        {
            // If the console logging is disable, will enable the feature
            if (!EnableConsoleLogging) EnableConsoleLogging = true;

            // If the Writing configurations object is null will return the current configuration
            if (WriteLogConfigurations == null) return this;

            // Add the new writing configuration inside the dedicated object
            WriteLogConfigurations.Add((WriteLogToConfiguration)new WriteLogToConfiguration(LoggingType.Console).Console(minimumLevel, restrictedToLevel));
            
            // Return the current configuration
            return this;
        }

        /// <summary>
        /// Add a new write to file settings to the current configuration
        /// </summary>
        /// <param name="fileName">Indicates the name of the log file that will be created</param>
        /// <param name="outputPath">Indicates the output path that will be used to create the log file</param>
        /// <param name="minimumLevel">Indicates the minimum logging level that will be logged to the console</param>
        /// <param name="restrictedToLevel">Indicates the restricted level that will be logged to the console, this will override the other parameter</param>
        /// <returns></returns>
        public LoggerConfiguration WriteToFile(
            string fileName = "", 
            string outputPath = "", 
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        )
        {
            // If the file logging is disable, will enable the feature
            if (!EnableFileLogging) EnableFileLogging = true;

            // If the Writing configurations object is null will return the current configuration
            if (WriteLogConfigurations == null) return this;

            // Add the new writing configuration inside the dedicated object
            var writeConfig = (WriteLogToConfiguration)new WriteLogToConfiguration(LoggingType.File).File(fileName, outputPath, minimumLevel, restrictedToLevel);
            WriteLogConfigurations.Add(writeConfig);

            // Return the current configuration
            return this;
        }

        /// <summary>
        /// Build a new Logger from the current configuration
        /// </summary>
        /// <returns>A new logger object generated from the current configuration</returns>
        public Logger Build()
        {
            return new Logger(this);
        }
    }
}
