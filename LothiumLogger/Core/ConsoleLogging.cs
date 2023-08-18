// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Core
{
    /// <summary>
    /// Class Dedicated To The Console Logging Methods 
    /// </summary>
    internal sealed class ConsoleLogging
	{
        /// <summary>
        /// Write a new log inside the Console
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        internal static void WriteToConsole(LogEventObject logEvent)
		{
			if (logEvent != null && !String.IsNullOrEmpty(logEvent.EventMessage))
			{
                Console.WriteLine(FormatManager.FormatLogMessage(logEvent, LogDateFormat.Standard));
			}
		}

        /// <summary>
        /// Write a list of logs inside the Console
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        internal static void WriteToConsole(List<LogEventObject> logEvents)
		{
			if (logEvents != null && logEvents.Count() > 0)
			{
				foreach (var logEvent in logEvents)
				{
					WriteToConsole(logEvent);
                }
            }
		}
    }
}

