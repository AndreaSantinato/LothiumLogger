// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Defines a console's theme class
    /// </summary>
    internal interface IConsoleSinkerThemes
    {
        /// <summary>
        /// Retrive the console's background color 
        /// </summary>
        /// <returns>A Specific Console's Backgroud Color</returns>
        ConsoleColor GetConsoleBackgroudColor();

        /// <summary>
        /// Retrive the text fore color for a specific log level
        /// </summary>
        /// <param name="logLevel">Contains the log level</param>
        /// <returns>A Specific Console's Text Fore Color</returns>
        ConsoleColor GetTextForeColorByLogLevel(LogLevelEnum logLevel);
    }
}
