// System Class
using System;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger.Sinkers.Formatters
{
    /// <summary>
    /// Json Formatter Class For The Log Event With Object Serialization
    /// </summary>
    internal static class JsonFormatter
    {
        /// <summary>
        /// Format an object into a json string 
        /// </summary>
        /// <param name="obj">Contains the object to serialize into a json string</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>A Json Formatted String</returns>
        public static ILogEvent FormatObject(object obj, ILogEvent logEvent)
        {
            string result = string.Empty;

            // Verify if the message of the current log event is not valorized
            var logMessage = logEvent.Message;
            if (string.IsNullOrEmpty(logMessage))
            {
                return logEvent;
            }

            // Check the content of the log event message for serialize the object
            var searchValue = string.Concat("{@", obj.GetType().Name, "}");
            if (logMessage.Contains(searchValue))
            {
                // Serialize all the object
                logMessage = logMessage.Replace(searchValue, JsonSerializer.Serialize(obj));
            }
            else
            {
                // Serialize a specific property
                var matches = new Regex(@"\{([^\}]+)\}").Matches(logEvent.Message);
                foreach (var match in matches)
                {
                    var matchValue = match.ToString()
                        .Replace("{", string.Empty)
                        .Replace("}", string.Empty)
                        .Replace("@", string.Empty);

                    // Verify if the variable contains the object type name or if it's only an external variabile
                    if (matchValue.Contains(obj.GetType().Name))
                    {
                        var pName = matchValue.Replace(string.Concat(obj.GetType().Name, "."), string.Empty);
                        var pValue = obj.GetType().GetProperty(pName).GetValue(obj, null).ToString();
                        logMessage = logMessage.Replace(match.ToString(), pValue);
                    }
                    else
                    {
                        logMessage = logMessage.Replace(match.ToString(), obj.ToString());
                    }
                }
            }

            // Set the new format message inside the log event's object
            logEvent.Message = logMessage;

            // Return new log event
            return logEvent;
        }

        /// <summary>
        /// Format an exception into a json string
        /// </summary>
        /// <param name="ex">Contains the exception to serialize into a json string</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>A Json Formatted String</returns>
        public static ILogEvent FormatException(Exception ex, ILogEvent logEvent)
        {
            string result = JsonSerializer.Serialize(ex);
            if (string.IsNullOrEmpty(logEvent.Message))
            {
                switch (logEvent.Level)
                {
                    case LogLevelEnum.Err:
                        logEvent.Message = "Exception Error: ";
                        break;
                    case LogLevelEnum.Fatal:
                        logEvent.Message = "Fatal Exception Error: ";
                        break;
                }
            }
            logEvent.Message = string.Format("{0}: {1}", logEvent.Message, result);
            return logEvent;
        }
    }
}
