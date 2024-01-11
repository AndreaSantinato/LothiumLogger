// System Classes
using System;
using System.Linq;
// Custom Classes
using LothiumLogger.Enumerations;
using LothiumLogger.Records;

// Configurations Namespace
namespace LothiumLogger.Configurations;

/// <summary>
/// Contains a set of setting's values for the sink's classes
/// </summary>
public class SinkOptions
{
    /// <summary>
    /// Indicates if the sink is enabled
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Indicates the format of the log's date
    /// </summary>
    public LogDateFormatEnum DateFormat { get; set; } = LogDateFormatEnum.Standard;

    /// <summary>
    /// Contains the loggin rule for the sink
    /// </summary>
    public LoggingRule? LoggingRule { get; set; } = null;

    /// <summary>
    /// Contains the rule for the log file creation
    /// </summary>
    public FileRule? FileRule { get; set; } = null;

    /// <summary>
    /// Contains the theme that the console sink will use
    /// </summary>
    public ConsoleThemeEnum ConsoleTheme { get; set; } = ConsoleThemeEnum.Default;

    /// <summary>
    /// Defines a new instance of the SinkSettings class
    /// </summary>
    /// <param name="settings">Contains all the settings for the sink</param>
    public SinkOptions() { }

    /// <summary>
    /// Defines a new instance of the SinkSettings class
    /// </summary>
    /// <param name="settings">Contains all the settings for the sink</param>
    internal SinkOptions(Action<SinkOptions> options)
    {
        options?.Invoke(this);
    }
}
