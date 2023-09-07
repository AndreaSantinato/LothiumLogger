// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Sinkers.Themes
{
    /// <summary>
    /// Define the LothiumDark Theme used by the ConsoleSinker
    /// </summary>
    internal class LothiumDark : IConsoleSinkerThemes
    {
        private ConsoleColor _backgroudColor { get; set; }
        private Dictionary<LogLevelEnum, ConsoleColor>? _textForeColors { get; set; }

        /// <summary>
        /// Default Class Constructor
        /// </summary>
        public LothiumDark()
        {
            _backgroudColor = ConsoleColor.Black;
            _textForeColors = new Dictionary<LogLevelEnum, ConsoleColor>();
            _textForeColors.Add(LogLevelEnum.Normal, ConsoleColor.Black);
            _textForeColors.Add(LogLevelEnum.Debug, ConsoleColor.Cyan);
            _textForeColors.Add(LogLevelEnum.Info, ConsoleColor.Green);
            _textForeColors.Add(LogLevelEnum.Warn, ConsoleColor.Yellow);
            _textForeColors.Add(LogLevelEnum.Err, ConsoleColor.Red);
            _textForeColors.Add(LogLevelEnum.Fatal, ConsoleColor.Magenta);
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
