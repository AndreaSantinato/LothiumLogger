// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;
using LothiumLogger.Configurations;

// Main Namespace
namespace LothiumLogger;

/// <summary>
/// Defines a new Logger instance
/// </summary>
public sealed class Logger : ILogger, IDisposable
{
    private bool _disposed = false;
    private static LoggerSettings? _settings;

    #region Class Constructors & Destructors

    /// <summary>
    /// Logger Instance Constructor
    /// </summary>
    /// <param name="settings">Contains the settings used to create a new logger's instance</param>
    public Logger(LoggerSettings settings) => _settings = settings;

    /// <summary>
    /// Logger Instance Constructor
    /// </summary>
    /// <param name="settings">Contains the delegate action for the settings used to create a new logger's instance</param>
    public Logger(Action<LoggerSettings> settings) => _settings = new LoggerSettings(settings);

    /// <summary>
    /// Dispose the current Logger instance
    /// </summary>
    public void Dispose()
    {
        DisposeResource(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Destructor to ensure that Dispose is called if the user forgets
    /// </summary>
    ~Logger() => DisposeResource(false);

    #endregion

    #region Core Methods (Synchronous & Asynchronous)

    /// <summary>
    /// Perform the disposing of all used resources inside this instance
    /// </summary>
    /// <param name="disposing">Indicates if the resource must be disposed</param>
    private void DisposeResource(bool disposing)
    {
        // If class was alredy disposed before exit the method
        if (_disposed) return;

        // Perform the actual disposing methods
        if (disposing) _settings = null;

        // Set the flag that defines this instance already disposed
        _disposed = true;
    }

    /// <summary>
    /// Manage the writing of a new log based on the logger configuration and its writing settings
    /// </summary>
    /// <param name="logEvent">Contains the log event to write</param>
    /// <param name="obj">Contains an optional object to be serialized</param>
    private void WriteLogEvent(LogEvent logEvent, object? obj = null)
    {
        // Validate required parameters
        LoggerValidator.ThrowIfLoggerSettingsNull(_settings, nameof(_settings), true);
        LoggerValidator.ThrowIfLogEventNull((LogEvent)logEvent, nameof(logEvent), true);
        
        // Check if there is an object to serialize
        if (obj is not null)
        {
            logEvent = obj.GetType() == typeof(Exception) 
                ? LogEvent.FormatFromException((Exception)obj, logEvent.Level) 
                : LogEvent.FormatFromObject(obj, logEvent.Message!, logEvent.Level);
        }

        // Perform the write method for every single sink configured
        _settings!.Sinks!.ForEach(x => {
            if (x.ValidateWriting(logEvent)) x.Write(logEvent);
        });
    }

    /// <summary>
    /// Manage the asynchronous writing of a new log based on the logger configuration and its writing settings
    /// </summary>
    /// <param name="logEvent">Contains the log event to write</param>
    /// <param name="obj">Contains an optional object to be serialized</param>
    private async Task WriteLogEventAsync(LogEvent logEvent, object? obj = null)
        => await Task.Run(() => WriteLogEvent(logEvent, obj));

    #endregion

    #region Default Log Methods (Synchronous)

    /// <summary>
    /// Write a normal log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Write(string message, object? obj = null)
        => Write(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a normal log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Write(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    #endregion
    #region Default Log Methods (Asynchronous)

    /// <summary>
    /// Write a normal log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task WriteAsync(string message, object? obj = null)
        => await WriteAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a normal log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task WriteAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    #endregion

    #region Debug Log Methods (Synchronous)

    /// <summary>
    /// Write a debug log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Debug(string message, object? obj = null)
        => Debug(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Debug, message), obj);

    /// <summary>
    /// Write a normal log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Debug(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    #endregion
    #region Debug Log Methods (Asynchronous)

    /// <summary>
    /// Write a debug log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task DebugAsync(string message, object? obj = null)
        => await DebugAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a debug log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task DebugAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    #endregion

    #region Information Log Methods (Synchronous)

    /// <summary>
    /// Write a information log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Information(string message, object? obj = null)
        => Information(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Info, message), obj);

    /// <summary>
    /// Write a information log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Information(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    #endregion
    #region Information Log Methods (Asynchronous)

    /// <summary>
    /// Write a information log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task InformationAsync(string message, object? obj = null)
        => await InformationAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a information log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task InformationAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    #endregion

    #region Warning Log Methods (Synchronous)

    /// <summary>
    /// Write a warning log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Warning(string message, object? obj = null)
        => Warning(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Warn, message), obj);

    /// <summary>
    /// Write a information log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Warning(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    #endregion
    #region Warning Log Methods (Asynchronous)

    /// <summary>
    /// Write a warning log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task WarningAsync(string message, object? obj = null)
        => await WarningAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a warning log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task WarningAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    #endregion

    #region Error Log Methods (Synchronous)

    /// <summary>
    /// Write an error log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Error(string message, object? obj = null)
        => Error(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Err, message), obj);

    /// <summary>
    /// Write a error log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Error(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    public void Error(Exception exception)
        => Error(exception, new LogEvent(DateTimeOffset.Now, LogLevelEnum.Err, "Exception Occured"));

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    /// <param name="logEvent">Contains the log event</param>
    public void Error(Exception exception, LogEvent logEvent)
        => Error(logEvent, exception);

    #endregion
    #region Error Log Methods (Asynchronous)

    /// <summary>
    /// Write a error log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task ErrorAsync(string message, object? obj = null)
        => await ErrorAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a error log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task ErrorAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    public async Task ErrorAsync(Exception exception)
        => await ErrorAsync(exception, new LogEvent(DateTimeOffset.Now, LogLevelEnum.Err, "Exception Occured"));

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    /// <param name="logEvent">Contains the log event</param>
    public async Task ErrorAsync(Exception exception, LogEvent logEvent)
        => await ErrorAsync(logEvent, exception);

    #endregion

    #region Fatal Log Methods (Synchronous)

    /// <summary>
    /// Write a fatal log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Fatal(string message, object? obj = null)
        => Fatal(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Fatal, message), obj);

    /// <summary>
    /// Write a fatal log
    /// </summary>
    /// <param name="logEvent">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public void Fatal(LogEvent logEvent, object? obj = null)
        => WriteLogEvent(logEvent, obj);

    /// <summary>
    /// Write a fatal log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    public void Fatal(Exception exception)
        => Fatal(exception, new LogEvent(DateTimeOffset.Now, LogLevelEnum.Fatal, "Fatal Exception Occured"));

    /// <summary>
    /// Write an fatal log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    /// <param name="logEvent">Contains the log event</param>
    public void Fatal(Exception exception, LogEvent logEvent)
        => Fatal(logEvent, exception);

    #endregion
    #region Fatal Log Methods (Asynchronous)

    /// <summary>
    /// Write a error log
    /// </summary>
    /// <param name="message">Contains the log message</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task FatalAsync(string message, object? obj = null)
        => await FatalAsync(new LogEvent(DateTimeOffset.Now, LogLevelEnum.Normal, message), obj);

    /// <summary>
    /// Write a error log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="obj">Contains the optional object to serialize into the log</param>
    public async Task FatalAsync(LogEvent logEvent, object? obj = null)
        => await WriteLogEventAsync(logEvent, obj);

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    public async Task FatalAsync(Exception exception)
        => await FatalAsync(exception, new LogEvent(DateTimeOffset.Now, LogLevelEnum.Err, "Exception Occured"));

    /// <summary>
    /// Write an error log from an exception
    /// </summary>
    /// <param name="exception">Contains the occured exception</param>
    /// <param name="logEvent">Contains the log event</param>
    public async Task FatalAsync(Exception exception, LogEvent logEvent)
        => await FatalAsync(logEvent, exception);

    #endregion
}