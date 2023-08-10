using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LothiumLogger.Enumerations
{
    /// <summary>
    /// Define the Logging Level
    /// </summary>
    public enum LogLevel
    {
        Normal = 0,
        Debug = 1, 
        Info = 2,
        Warn = 3,
        Err = 4,
        Fatal = 5,
    }

    /// <summary>
    /// Defines the type of the logging type
    /// </summary>
    public enum LoggingType
    {
        Console = 0,
        File = 1,
    }

    /// <summary>
    /// Define the type of Logging Output Format
    /// </summary>
    public enum LogFormat
    {
        Easy = 0,
        Full = 1,
    }

    /// <summary>
    /// Define the type of date of the Log Message
    /// </summary>
    internal enum LogDateFormat
    {
        Standard = 0,
        Minimal = 1,
        Full = 3,
    }

    /// <summary>
    /// Define the Logging File's Status
    /// </summary>
    internal enum LogErrorType
    {
        None = 0,
        NotUsable = 1,
        WithoutFileName = 2,
        WithoutOutputDirectory = 3,
        WithoutEvents = 4,
        ErrorOnReading = 5,
        ErrorOnWriting = 6,
    }
}
