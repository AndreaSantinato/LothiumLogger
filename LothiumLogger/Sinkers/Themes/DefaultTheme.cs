// Custom Classes
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

// Themes Namespace
namespace LothiumLogger.Sinkers.Themes;

/// <summary>
/// Defines the Default Console Sink's Theme
/// </summary>
internal class DefaultTheme : IGenericSink
{
    protected ConsoleColor BackgroudColor { get; set; }
    protected Dictionary<LogLevelEnum, ConsoleColor> ForegroundColors { get; set; }

    /// <summary>
    /// Defines the colors for the Default Theme
    /// </summary>
    internal DefaultTheme()
    {
        BackgroudColor = ConsoleColor.Black;
        ForegroundColors = new Dictionary<LogLevelEnum, ConsoleColor>()
        {
            [LogLevelEnum.Normal] = ConsoleColor.White,
            [LogLevelEnum.Debug] = ConsoleColor.White,
            [LogLevelEnum.Info] = ConsoleColor.White,
            [LogLevelEnum.Warn] = ConsoleColor.White,
            [LogLevelEnum.Err] = ConsoleColor.White,
            [LogLevelEnum.Fatal] = ConsoleColor.White
        };
    }

    /// <summary>
    /// Get the backgroud's color for the console
    /// </summary>
    /// <returns></returns>
    public ConsoleColor GetBackgroundColor() => BackgroudColor;

    /// <summary>
    /// Get the text's color for the console
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public ConsoleColor ForegroundColor(LogLevelEnum level) => ForegroundColors[level];
}
