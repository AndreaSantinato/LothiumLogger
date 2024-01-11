// System Classes
using System;
// Custom Classes
using LothiumLogger.Interfaces;
using LothiumLogger.Sinkers;

// Configurations Namespace
namespace LothiumLogger.Configurations;

public class LoggerSettings
{
    internal List<ISink>? Sinks { get; set; } = new List<ISink>();

    /// <summary>
    /// Defines an instance of the LoggerSettings class
    /// </summary>
    /// <param name="settings">Contains the delegate action that defines the configured settings</param>
    public LoggerSettings(Action<LoggerSettings> settings) => settings?.Invoke(this);

    /// <summary>
    /// Generic operation to add a new sink
    /// </summary>
    /// <param name="sink">Contains the actual sink</param>
    /// <returns>An updated logger settings</returns>
    public LoggerSettings AddSink(ISink sink)
    {
        if (sink is not null) Sinks?.Add(sink);
        return this;
    }

    /// <summary>
    /// Add a new Console's Sink to the current settings
    /// </summary>
    /// <param name="options">Contains the sink dedicated options</param>
    /// <returns>An updated logger settings</returns>
    public LoggerSettings AddConsoleSink(Action<SinkOptions> options) 
        => AddSink(new GenericSink(new SinkOptions(options)));

    /// <summary>
    /// Add a new File's Sink to the current settings
    /// </summary>
    /// <param name="options">Contains the sink dedicated options</param>
    /// <returns>An updated logger settings</returns>
    public LoggerSettings AddFileSink(Action<SinkOptions> options) 
        => AddSink(new FileSink(new SinkOptions(options)));
}
