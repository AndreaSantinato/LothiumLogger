// Custom Classes
using LothiumLogger.Enumerations;

// Records Namespace
namespace LothiumLogger.Records;

/// <summary>
/// Defines the rule for the logs management
/// </summary>
/// <param name="Level">Contains the logging level</param>
/// <param name="Restricted">Indicates if the writing event must match the logging level rule</param>
public record LoggingRule(LogLevelEnum MinimumLevel, bool Restricted);

/// <summary>
/// Defines the rule for the log files generation
/// </summary>
/// <param name="Type">Contains the type of the log file</param>
/// <param name="Path">Contains the path of the log file</param>
/// <param name="Name">Contains the name of the log file</param>
public record FileRule(LogFileTypeEnum Type, string Path, string Name = "");
