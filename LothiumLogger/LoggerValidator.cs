// System Classes
using System;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
// Custom Classes
using LothiumLogger.Configurations;

// Main Namespace
namespace LothiumLogger;

/// <summary>
/// Validator Class For All The Project Main Classes
/// </summary>
internal class LoggerValidator
{
    /// <summary>
    /// Throw an ArgumentNullException if the logger's settings object or one of its required property is null
    /// </summary>
    /// <param name="settings">Contains the actual logger's settings</param>
    /// <param name="paramName">Contains the parameter name for the exception caller</param>
    /// <param name="checkProperties">Indicates if the method need to verify the properties</param>
    internal static void ThrowIfLoggerSettingsNull([NotNull] LoggerSettings? settings, string? paramName = "", bool checkProperties = false)
    {
        if (settings is null)
            ThrowException(paramName);

        if (checkProperties)
        {
            if (settings.Sinks is null)
                ThrowException(nameof(settings.Sinks));
            else if (!settings.Sinks.Any())
                ThrowException(paramName);
        }
    }

    /// <summary>
    /// Throw an ArgumentNullException if the logger sink's options object is null
    /// </summary>
    /// <param name="options">Contains the actual logger sink's options</param>
    /// <param name="paramName">Contains the parameter name for the exception caller</param>
    internal static void ThrowIfSinkOptionsNull([NotNull] SinkOptions? options, string? paramName = "")
    {
        if (options is null) 
            ThrowException(paramName);
    }

    /// <summary>
    /// Throw an ArgumentNullException if the log event object or one of its required property is null
    /// </summary>
    /// <param name="logEvent">Contains the actual log event</param>
    /// <param name="paramName">Contains the parameter name for the exception caller</param>
    /// <param name="checkProperties">Indicates if the method need to verify the properties</param>
    internal static void ThrowIfLogEventNull([NotNull] LogEvent? logEvent, string? paramName = "", bool checkProperties = false)
    {
        if (logEvent is null)
            ThrowException(paramName);

        if (checkProperties)
        {
            if (string.IsNullOrEmpty(logEvent.Message))
                ThrowException(nameof(logEvent.Message));
        }
    }

    /// <summary>
    /// Throw an ArgumentNullException if an object is null
    /// </summary>
    /// <param name="obj">Contains the actual object</param>
    /// <param name="paramName">Contains the parameter name for the exception caller</param>
    internal static void ThrowIfObjectNull([NotNull] object? obj, string? paramName = "")
    {
        if (obj is null)
            ThrowException(paramName);
    }

    [DoesNotReturn]
    private static void ThrowException(string? paramName) 
        => throw new ArgumentNullException(paramName);
}
