// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Sinkers.Themes
{
    /// <summary>
    /// Define the LothiumLight Theme used by the ConsoleSinker
    /// </summary>
    internal class LothiumLight : IConsoleSinkerThemes
    {
        private ConsoleColor _backgroudColor { get; set; }
        private Dictionary<LogLevelEnum, ConsoleColor>? _textForeColors { get; set; }

        /// <summary>
        /// Default Class Constructor
        /// </summary>
        public LothiumLight()
        {
            _backgroudColor = ConsoleColor.White;
            _textForeColors = new Dictionary<LogLevelEnum, ConsoleColor>();
            _textForeColors.Add(LogLevelEnum.Normal, ConsoleColor.Black);
            _textForeColors.Add(LogLevelEnum.Debug, ConsoleColor.DarkCyan);
            _textForeColors.Add(LogLevelEnum.Info, ConsoleColor.DarkGreen);
            _textForeColors.Add(LogLevelEnum.Warn, ConsoleColor.DarkYellow);
            _textForeColors.Add(LogLevelEnum.Err, ConsoleColor.DarkRed);
            _textForeColors.Add(LogLevelEnum.Fatal, ConsoleColor.DarkMagenta);
        }

        /// <summary>
        /// Retrive the console's background color 
        /// </summary>
        /// <returns>A Specific Console's Backgroud Color</returns>
        public ConsoleColor GetConsoleBackgroudColor() => _backgroudColor;

        /// <summary>
        /// Retrive the text fore color for a specific log level
        /// </summary>
        /// <param name="logLevel">Contains the log level</param>
        /// <returns>A Specific Console's Text Fore Color</returns>
        public ConsoleColor GetTextForeColorByLogLevel(LogLevelEnum logLevel) => _textForeColors.GetValueOrDefault(logLevel);
    }
}
