// Custom Classes
using LothiumLogger.Enumerations;

// Themes Namespace
namespace LothiumLogger.Sinkers.Themes;

/// <summary>
/// Defines the Light Console Sink's Theme
/// </summary>
internal sealed class LightTheme : DefaultTheme
{
    /// <summary>
    /// Defines the colors for the Light Theme
    /// </summary>
    internal LightTheme() : base()
    {
        BackgroudColor = ConsoleColor.White;
        ForegroundColors[LogLevelEnum.Normal] = ConsoleColor.Black;
        ForegroundColors[LogLevelEnum.Debug] = ConsoleColor.DarkCyan;
        ForegroundColors[LogLevelEnum.Info] = ConsoleColor.DarkGreen;
        ForegroundColors[LogLevelEnum.Warn] = ConsoleColor.DarkYellow;
        ForegroundColors[LogLevelEnum.Err] = ConsoleColor.DarkRed;
        ForegroundColors[LogLevelEnum.Fatal] = ConsoleColor.DarkMagenta;
    }
}
