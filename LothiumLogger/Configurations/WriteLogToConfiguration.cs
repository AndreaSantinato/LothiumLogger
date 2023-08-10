// Custom Class
using System;
using LothiumLogger.Core;
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger.Configurations
{
    public class WriteLogToConfiguration : IWriteLogToConfiguration
    {
        public LoggingType Type { get; internal set; }

        public LogLevel MinimumLogLevel { get; internal set; }

        public LogLevel RestrictedToLogLevel { get; internal set; }
        
        public string? FileName { get; internal set; }
        
        public string? FilePath { get; internal set; }

        /// <summary>
        /// Generate a new Log Writing Configuration
        /// </summary>
        public WriteLogToConfiguration(LoggingType type)
        {
            Type = type;
        }

        /// <see cref="ILoggerWriteToConfiguration"/>
        public IWriteLogToConfiguration Console(LogLevel minimumLevel, LogLevel restrictedToLevel)
        {
            FileName = String.Empty;
            FilePath = String.Empty;
            MinimumLogLevel = minimumLevel;
            RestrictedToLogLevel = restrictedToLevel;
            return this;
        }

        /// <see cref="ILoggerWriteToConfiguration"/>
        /// <returns><see cref="ILoggerWriteToConfiguration"/></returns>
        public IWriteLogToConfiguration File(string fileName, string filePath, LogLevel minimumLevel, LogLevel restrictedToLevel)
        {
            if (String.IsNullOrEmpty(fileName)) 
            {
                FileName = String.Format("Log_{0}", FormatManager.FormatLogDate(LogDateFormat.Minimal, DateTime.Now));
            }
            else
            {
                FileName = fileName;
            }
            
            if (String.IsNullOrEmpty(filePath))
            {
                FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Log");
            }
            else
            {
                FilePath = filePath;
            }
            MinimumLogLevel = minimumLevel;
            RestrictedToLogLevel = restrictedToLevel;
            return this;
        }
    }
}

