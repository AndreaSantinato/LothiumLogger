// Custom Classes
using LothiumLogger.Enumerations;

// Themes Namespace
namespace LothiumLogger.Sinkers.Themes;

/// <summary>
/// Defines the Dark Console Sink's Theme
/// </summary>
internal sealed class DarkTheme : DefaultTheme
{
    /// <summary>
    /// Defines the colors for the Dark Theme
    /// </summary>
    internal DarkTheme() : base()
    {
        BackgroudColor = ConsoleColor.Black;
        ForegroundColors[LogLevelEnum.Normal] = ConsoleColor.White;
        ForegroundColors[LogLevelEnum.Debug] = ConsoleColor.Cyan;
        ForegroundColors[LogLevelEnum.Info] = ConsoleColor.Green;
        ForegroundColors[LogLevelEnum.Warn] = ConsoleColor.Yellow;
        ForegroundColors[LogLevelEnum.Err] = ConsoleColor.Red;
        ForegroundColors[LogLevelEnum.Fatal] = ConsoleColor.Magenta;
    }
}
