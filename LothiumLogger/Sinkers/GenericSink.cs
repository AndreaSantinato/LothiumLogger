// Custom Classes
using LothiumLogger.Interfaces;
using LothiumLogger.Enumerations;
using LothiumLogger.Configurations;
using LothiumLogger.Sinkers.Themes;

// Sinkers Namespace
namespace LothiumLogger.Sinkers;

/// <summary>
/// Define a GenericSink instance
/// Provides a set of methods to write log events inside the OS/IDE console
/// </summary>
public class GenericSink : ISink, IDisposable
{
    private bool _disposed = false;
    protected SinkOptions? Options;

    #region Class Constructors

    /// <summary>
    /// Defines a new GenericSink instance
    /// </summary>
    /// <param name="options">Contains all the options for the sink</param>
    public GenericSink(SinkOptions options)
        => Options = options;


    /// <summary>
    /// Defines a new GenericSink instance
    /// </summary>
    /// <param name="options">Contains all the options for the sink</param>
    public GenericSink(Action<SinkOptions> options)
        => Options = new SinkOptions(options);

    /// <summary>
    /// Dispose the current sink instance
    /// </summary>
    public void Dispose()
    {
        DisposeResource(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Destructor to ensure that Dispose is called if the user forgets
    /// </summary>
    ~GenericSink() => DisposeResource(false);

    #endregion

    #region Class Methods

    /// <summary>
    /// Perform the disposing of all used resources inside this instance
    /// </summary>
    /// <param name="disposing">Indicates if the resource must be disposed</param>
    protected void DisposeResource(bool disposing)
    {
        // If class was already disposed before exit the method
        if (_disposed) return;

        // Perform the actual disposing methods
        if (disposing) Options = null;

        // Set the flag that defines this instance already disposed
        _disposed = true;
    }

    /// <summary>
    /// Generate a final message formatted based on a specific setting rules
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="previouslyWrittenLogEvents">Contains a previously written log events</param>
    /// <returns>A Formatted Message</returns>
    public virtual string FormatMessage(LogEvent logEvent, string previouslyWrittenLogEvents = "")
    {
        var content = string.Format(
            "[{0}] [{1}]: {2}",
            GenerateLogDate(logEvent.Date, Options!.DateFormat),
            Enum.GetName(typeof(LogLevelEnum), logEvent.Level),
            logEvent.Message
        );

        if (!string.IsNullOrEmpty(previouslyWrittenLogEvents))
            content = string.Concat(previouslyWrittenLogEvents, Environment.NewLine, content);

        return content;
    }

    /// <summary>
    /// Format the date that will be used for the log based on a choosen format's type
    /// </summary>
    /// <param name="date">Contains the actual date</param>
    /// <param name="dateFormat">Contains the date of the format</param>
    /// <returns>A String formatted based on the passed Date Format</returns>
    public virtual string GenerateLogDate(DateTimeOffset date, LogDateFormatEnum dateFormat)
    {
        return dateFormat switch
        {
            LogDateFormatEnum.Standard => date.ToString("yyyy/MM/dd hh:mm:ss"),
            LogDateFormatEnum.Minimal => date.ToString("yyyy/MM/dd"),
            LogDateFormatEnum.Full => $"({date.Year}) {date.Month} {date.Day} {date:HH:mm:ss}",
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Validate all the minimum requirements for the writing method
    /// </summary>
    /// <param name="logEvent">Contains the generated log event</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If one requirement is not valid the method will generated an exception</exception>
    public bool ValidateWriting(LogEvent logEvent)
    {
        LoggerValidator.ThrowIfSinkOptionsNull(Options, nameof(Options));
        LoggerValidator.ThrowIfObjectNull(Options.LoggingRule, nameof(Options.LoggingRule));
        LoggerValidator.ThrowIfLogEventNull(logEvent, nameof(logEvent), true);

        if (!Options.Enabled) return false;
        if (Options.LoggingRule.Restricted && Options.LoggingRule.MinimumLevel != logEvent.Level) return false;
        if (logEvent.Level < Options.LoggingRule.MinimumLevel) return false;

        return true;
    }

    /// <summary>
    /// Write a new log to the console
    /// </summary>
    /// <param name="logEvent">Contains the generated log event</param>
    public virtual void Write(LogEvent logEvent)
    {
        // Define the theme for the console sink
        IGenericSink? theme = Options!.ConsoleTheme switch
        {
            ConsoleThemeEnum.Default => new DefaultTheme(),
            ConsoleThemeEnum.Light => new LightTheme(),
            ConsoleThemeEnum.Dark => new DarkTheme(),
            _ => null
        };
        LoggerValidator.ThrowIfObjectNull(theme, nameof(theme));

        // Set the colors and write the log event
        Console.BackgroundColor = theme.GetBackgroundColor();
        Console.ForegroundColor = theme.ForegroundColor(logEvent.Level);
        Console.WriteLine(FormatMessage(logEvent));
    }

    #endregion
}
