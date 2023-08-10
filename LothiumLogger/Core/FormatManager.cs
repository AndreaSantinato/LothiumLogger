// System Class
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Core
{


    /// <summary>
    /// Class Dedicated To The Generation Of The Log Content
    /// </summary>
	internal sealed class FormatManager
    {
        /// <summary>
        /// Return the current month name based on the date month's value
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static string GetMonthNameFromDate(int monthValue)
        {
            string monthName = String.Empty;
            switch (monthValue)
            {
                #region Genuary

                case 1:
                    monthName = "Gen";
                    break;

                #endregion
                #region February

                case 2:
                    monthName = "Feb";
                    break;

                #endregion
                #region March

                case 3:
                    monthName = "Mar";
                    break;

                #endregion
                #region April
                
                case 4:
                    monthName = "Apr";
                    break;

                #endregion
                #region May

                case 5:
                    monthName = "May";
                    break;

                #endregion
                #region June

                case 6:
                    monthName = "Jun";
                    break;

                #endregion
                #region July

                case 7:
                    monthName = "Jul";
                    break;

                #endregion
                #region August

                case 8:
                    monthName = "Aug";
                    break;

                #endregion
                #region September

                case 9:
                    monthName = "Sep";
                    break;

                #endregion
                #region October
                
                case 10:
                    monthName = "Oct";
                    break;

                #endregion
                #region November

                case 11:
                    monthName = "Nov";
                    break;

                #endregion
                #region December

                case 12:
                    monthName = "Dec";
                    break;

                #endregion
            }
            return monthName;
        }

        /// <summary>
        /// Format the date that will be used for the log based on a choosen format's type
        /// </summary>
        /// <param name="dateFormat">Choosen Date Format</param>
        /// <returns>A String formatted based on the passed Date Fornat</returns>
        internal static string FormatLogDate(LogDateFormat dateFormat, DateTime date)
        {
            string result = String.Empty;

            switch (dateFormat)
            {
                case LogDateFormat.Minimal:
                    result = date.ToString("yyyymmdd");
                    break;
                case LogDateFormat.Standard:
                    result = date.ToString("yyyy/mm/dd hh:mm:ss");
                    break;
                case LogDateFormat.Full:
                    result = String.Format("({0}) {1} {2} {3}", date.Year, GetMonthNameFromDate(date.Month), date.Day, date.ToString("hh:mm:ss"));
                    break;
            }

            return result;
        }

        /// <summary>
        /// Format a new Log File
        /// Conbine an existing file content with a new content
        /// </summary>
        /// <param name="existingContent">Contains the actual file content</param>
        /// <param name="newContent">Contains the new content to be added to the file</param>
        /// <returns>A combined content to write inside the file</returns>
        internal static string FormatLogFile(string existingContent, string newContent)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(newContent)) return string.Empty;
            if (!String.IsNullOrEmpty(existingContent))
            {
                result = String.Concat(existingContent, Environment.NewLine, newContent);
            }
            else
            {
                result = newContent;
            }
            return result;
        }

        /// <summary>
        /// Generate a log content with normal format
        /// </summary>
        /// <param name="logEvent">Contains the log event generated</param>
        /// <param name="dateFormat">Contains the log date format</param>
        /// <returns></returns>
		internal static string FormatLogMessage(LogEventObject logEvent, LogDateFormat dateFormat)
        {
            if (logEvent == null) return String.Empty;
            if (String.IsNullOrEmpty(logEvent.EventMessage)) return String.Empty;

            var date = FormatLogDate(dateFormat, logEvent.EventDate);
            var level = Enum.GetName(typeof(LogLevel), logEvent.EventLevel);
            var message = logEvent.EventMessage;

            return String.Format("[{0}] [{1}]: {2}", date, level, message);
        }

        /// <summary>
        /// Generate a log message with an object serialization
        /// </summary>
        /// <param name="obj">Contains the object to serialize into the log message</param>
        /// <param name="logEvent">Contains the log event generated</param>
        /// <param name="dateFormat">Contains the log date format</param>
        /// <returns></returns>
        internal static string FormatLogMessageFromObject(object obj, LogEventObject logEvent, LogDateFormat dateFormat)
        {
            logEvent.EventMessage = new Regex(@"\{([^\}]+)\}").Replace(logEvent.EventMessage, JsonSerializer.Serialize(obj));
            return FormatManager.FormatLogMessage(logEvent, dateFormat);
        }

        /// <summary>
        /// Generate a new log content with a custom complete format
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        internal static string FormatFullLog(List<LogEventObject> logEvents, LogDateFormat dateFormat, string existedFileContent = "")
        {
            var innerContent = String.Empty;

            if (!String.IsNullOrEmpty(existedFileContent)) innerContent = String.Concat(existedFileContent, Environment.NewLine);

            foreach (var logEvent in logEvents)
            {
                innerContent = String.Concat("\t(", FormatManager.FormatLogMessage(logEvent, dateFormat), ");", Environment.NewLine);
            }

            var date = DateTime.Now.ToString("yy-MM-dd");
            var outContent = String.Format("EventsOfDate:({0})", date);

            return String.Concat(outContent, Environment.NewLine, "{", Environment.NewLine, innerContent, "}");
        }

        /// <summary>
        /// Generate a new LogEvent based from an Exception
        /// </summary>
        /// <param name="exception">Contains the occured exception</param>
        /// <param name="message">Contains an optional message</param>
        /// <returns></returns>
        internal static LogEventObject FormatLogFromException(Exception exception, LogLevel level, string message = "")
        {
            string result = JsonSerializer.Serialize(exception);
            if (!String.IsNullOrEmpty(message))
            {
                result = String.Concat(message, " ", result);
            }
            return new LogEventObject(DateTime.Now, level, result);
        }
    }
}

