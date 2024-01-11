// Interfaces Namespace
namespace LothiumLogger.Interfaces;

/// <summary>
/// Define the structure for a Sink Class
/// </summary>
public interface ISink
{
    /// <summary>
    /// Generate a formatted output message from a log
    /// </summary>
    /// <param name="logEvent">Contains the log event</param>
    /// <param name="previouslyWrittenLogEvents">Contains a previously written log events</param>
    /// <returns>A Formatted Message</returns>
    abstract string FormatMessage(LogEvent logEvent, string previouslyWrittenLogEvents = "");

    /// <summary>
    /// Validate all the minimum requirements for the writing method
    /// </summary>
    /// <param name="logEvent">Contains the generated log event</param>
    /// <returns>A True Or False Status Value</returns>
    abstract bool ValidateWriting(LogEvent logEvent);

    /// <summary>
    /// Write a new event
    /// </summary>
    /// <param name="logEvent">Contains the log</param>
    abstract void Write(LogEvent logEvent);
}
