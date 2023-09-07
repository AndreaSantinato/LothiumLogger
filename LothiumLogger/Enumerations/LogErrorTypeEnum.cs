// System Class
using System;

namespace LothiumLogger.Enumerations
{
    /// <summary>
    /// Define the Logging File's Status
    /// </summary>
    internal enum LogErrorTypeEnum
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
