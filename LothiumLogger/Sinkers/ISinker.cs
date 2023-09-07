// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Interface that define a generic Sink
    /// </summary>
    public interface ISinker : IDisposable
    {
        /// <summary>
        /// Return the sink's type
        /// </summary>
        /// <returns>Sink's Type</returns>
        SinkTypeEnum GetSinkType();

        /// <summary>
        /// Define if the sink is enabled for logging events
        /// </summary>
        /// <returns>True Or False Status</returns>
        bool IsSinkerEnabled();

        /// <summary>
        /// Return the minimum accepted log level accepted by the sink
        /// </summary>
        /// <returns>Minimum accepted log's level foreach log event</returns>
        LogLevelEnum GetMinimumLogLevel();

        /// <summary>
        /// Return the only accepted log level by the sink
        /// </summary>
        /// <returns>The only accepted log level foreach log event</returns>
        LogLevelEnum GetRestrictedLogLevel();

        /// <summary>
        /// Initialize the writing of a new log event
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        bool Initialize(ILoggerConfiguration configuration, ILogEvent logEvent);

        /// <summary>
        /// Write a new log event
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        void Write(ILogEvent logEvent);
    }
}
