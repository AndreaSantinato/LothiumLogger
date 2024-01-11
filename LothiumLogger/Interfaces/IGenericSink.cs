// Custom Classes
using LothiumLogger.Enumerations;

// INterfaces Namespace
namespace LothiumLogger.Interfaces;

internal interface IGenericSink
{
    ConsoleColor GetBackgroundColor();

    ConsoleColor ForegroundColor(LogLevelEnum level);
}
