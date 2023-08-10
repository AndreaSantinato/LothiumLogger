using LothiumLogger.Enumerations;
using System;

namespace LothiumLogger.Interfaces
{
    public interface ILoggerConfiguration
    {
        public LoggerConfiguration WriteToConsole(
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        );

        public LoggerConfiguration WriteToFile(
            string fileName = "",
            string outputPath = "",
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        );

        public Logger Build();
    }
}

