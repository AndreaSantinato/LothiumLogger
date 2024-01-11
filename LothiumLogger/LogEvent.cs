// System Class
using System.Text.Json;
using System.Text.RegularExpressions;
// Custom Class
using LothiumLogger.Interfaces;
using LothiumLogger.Enumerations;

// Main Namespace
namespace LothiumLogger;

/// <summary>
/// Object that represent a log event
/// </summary>
public sealed class LogEvent : ILogEvent, IDisposable
{
    private bool _disposed = false;
    
    #region Class Property
    
    /// <summary>
    /// Indicates the Date of the log event
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Indicates the level of the log event
    /// </summary>
    public LogLevelEnum Level { get; set; }

    /// <summary>
    /// Contains the message of the log event
    /// </summary>
    public string? Message { get; set; }

    #endregion

    #region Class Constructors & Destructors
    
    /// <summary>
    /// Defines a generic log event
    /// </summary>
    public LogEvent() : this(DateTimeOffset.Now, LogLevelEnum.Normal, string.Empty) { }

    /// <summary>
    /// Defines a specific log event
    /// </summary>
    /// <param name="date">Indicates the date when the log event occured</param>
    /// <param name="level">Indicates the level of the log event occured</param>
    /// <param name="message">Indicates the message of the log event occured</param>
    public LogEvent(DateTimeOffset date, LogLevelEnum level, string? message)
    {
        Date = date;
        Level = level;
        Message = message;
    }

    /// <summary>
    /// Defines a specific log event from an object
    /// </summary>
    /// <param name="obj">Contains the object to convert into a log event</param>
    /// <param name="message">Contains a message to the log event</param>
    /// <param name="level">Contains the level of the log event</param>
    public LogEvent(object obj, string message = "", LogLevelEnum level = LogLevelEnum.Normal) 
        : this(LogEvent.FormatFromObject(obj, message, level)) { }

    /// <summary>
    /// Defines a specific log event from an exception
    /// </summary>
    /// <param name="exception">Contains the exception to convert into a log event</param>
    /// <param name="errorLevel">Contains the exception level</param>
    public LogEvent(Exception exception, LogLevelEnum errorLevel) 
        : this(LogEvent.FormatFromException(exception, errorLevel)) { }

    /// <summary>
    /// Private constructor that populate a log event instance fron another instance
    /// This constructor is only used as a helper for the other constructor that generate
    /// a log event from an object or an exception
    /// </summary>
    /// <param name="logEvent">Contains a log event</param>
    private LogEvent(LogEvent logEvent)
    {
        this.Date = logEvent.Date;
        this.Level = logEvent.Level;
        this.Message = logEvent.Message;
    }

    /// <summary>
    /// Dispose the current LogEvent instance
    /// </summary>
    public void Dispose()
    {
        DisposeResource(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Destructor to ensure that Dispose is called if the user forgets
    /// </summary>
    ~LogEvent() => DisposeResource(false);
    
    #endregion

    #region Class Methods
    
    /// <summary>
    /// Perform the disposing of all used resources inside this instance
    /// </summary>
    /// <param name="disposing">Indicates if the resource must be disposed</param>
    private void DisposeResource(bool disposing)
    {
        // If class was alredy disposed before exit the method
        if (_disposed) return;

        // Perform the actual disposing methods
        if (disposing)
        {
            Date = DateTimeOffset.Now;
            Level = LogLevelEnum.Normal;
            Message = null;
        }

        // Set the flag that defines this instance already disposed
        _disposed = true;
    }
    
    /// <summary>
    /// Format an object into a json string 
    /// </summary>
    /// <param name="obj">Contains the object to serialize into a json string</param>
    /// <param name="message">Contains the log event's message</param>
    /// <param name="level">Contains the log event's level</param>
    /// <returns>A Json Formatted String</returns>
    public static LogEvent FormatFromObject(object obj, string message, LogLevelEnum level)
    {
        // Check the content of the log event message for serialize the object
        var searchValue = string.Concat("{@", obj.GetType().Name, "}");
        if (message.Contains(searchValue))
        {
            // Serialize all the object
            message = message.Replace(searchValue, JsonSerializer.Serialize(obj));
        }
        else
        {
            // Serialize a specific property
            var matches = new Regex(@"\{([^\}]+)\}").Matches(message);
            foreach (var match in matches)
            {
                var matchValue = match.ToString()!
                    .Replace("{", string.Empty)
                    .Replace("}", string.Empty)
                    .Replace("@", string.Empty);

                // Verify if the variable contains the object type name or if it's only an external variabile
                if (matchValue.Contains(obj.GetType().Name))
                {
                    var pName = matchValue.Replace(string.Concat(obj.GetType().Name, "."), string.Empty);
                    var pValue = obj.GetType().GetProperty(pName)?.GetValue(obj, null)?.ToString();
                    message = message.Replace(match.ToString()!, pValue);
                }
                else
                {
                    message = message.Replace(match.ToString()!, obj.ToString());
                }
            }
        }

        return new LogEvent(
            DateTimeOffset.Now,
            level,
            message
        );
    }

    /// <summary>
    /// Format an exception into a json string
    /// </summary>
    /// <param name="ex">Contains the exception to serialize into a json string</param>
    /// <param name="errorLevel">Contains the log error's level</param>
    /// <returns>A Json Formatted String</returns>
    public static LogEvent FormatFromException(Exception ex, LogLevelEnum errorLevel)
    {            
        var content = JsonSerializer.Serialize(ex);
        var title = errorLevel switch
        {
            LogLevelEnum.Err => "[Error]: ",
            LogLevelEnum.Fatal => "[Fatal Error]: ",
            _ => string.Empty
        };

        return new LogEvent(
            DateTimeOffset.Now, 
            errorLevel,
            $"{title}: {content}"
        );
    }
    
    #endregion
}
