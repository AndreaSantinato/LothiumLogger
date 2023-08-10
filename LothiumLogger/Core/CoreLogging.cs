// Custom Class
using LothiumLogger.Configurations;
using LothiumLogger.Enumerations;
using System.Reflection.Emit;

namespace LothiumLogger.Core
{
    /// <summary>
    /// Centralized Logging Class
    /// </summary>
    internal static class CoreLogging
    {
        /// <summary>
        /// Manage the writing of a new log based on the logger configuration and its writing settings
        /// </summary>
        /// <param name="loggerConfig">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event to write</param>
        /// <param name="obj">Contains an optional object to be serialized</param>
        internal static void WriteNewLog(
            LoggerConfiguration loggerConfig,
            LogEventObject logEvent,
            object obj
        )
        {
            if (loggerConfig == null) return;
            if (loggerConfig.WriteLogConfigurations == null) return;
            if (loggerConfig.WriteLogConfigurations.Count() == 0) return;
            if (logEvent == null) return;

            // If the object is not null it will format the message with the serialized object result
            if (obj != null)
            {
                logEvent.EventMessage = FormatManager.FormatLogMessageFromObject(obj, logEvent, LogDateFormat.Full);
            }

            foreach (var writeConfig in loggerConfig.WriteLogConfigurations)
            {
                // Check if the log can be processed or not
                if (writeConfig.RestrictedToLogLevel != LogLevel.Normal && logEvent.EventLevel != writeConfig.RestrictedToLogLevel) return;

                // Write the new log message based on the type of the write's configuration actually passed
                switch (writeConfig.Type)
                {
                    case LoggingType.Console:
                        if (!loggerConfig.EnableConsoleLogging) return;
                        if (logEvent.EventLevel >= writeConfig.MinimumLogLevel)
                        {
                            ConsoleLogging.WriteToConsole(logEvent);
                        }
                        break;
                    case LoggingType.File:
                        if (!loggerConfig.EnableFileLogging) return;
                        if (logEvent.EventLevel >= writeConfig.MinimumLogLevel)
                        {
                            FileLogging.WriteToFile(logEvent, writeConfig.FileName, writeConfig.FilePath);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Manage the writing of a new log based on an occured exception
        /// </summary>
        /// <param name="loggerConfig">Contains the current logger configuration</param>
        /// <param name="ex">Contains the occured exception</param>
        /// <param name="message">Contains a message</param>
        internal static void WriteNewLogFromException(
            LoggerConfiguration loggerConfig,
            Exception ex,
            LogLevel level,
            string message
        )
        {
            if (loggerConfig == null) return;
            if (loggerConfig.WriteLogConfigurations == null) return;
            if (loggerConfig.WriteLogConfigurations.Count() == 0) return;
            if (ex == null) return;
            if (String.IsNullOrEmpty(message)) 
            {
                message = "An error occured during the code execution:";
            }
            LogEventObject logEventObject = FormatManager.FormatLogFromException(ex, level, message);
            CoreLogging.WriteNewLog(loggerConfig, logEventObject, null);
        }
    }
}
