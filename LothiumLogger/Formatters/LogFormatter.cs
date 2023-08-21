// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Formatters
{
    /// <summary>
    /// Formatter Class For The Log Events
    /// </summary>
    internal static class LogFormatter
    {
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
		internal static string FormatLogMessage(LogEvent logEvent, LogDateFormat dateFormat)
        {
            if (logEvent == null) return String.Empty;
            if (String.IsNullOrEmpty(logEvent.Message)) return String.Empty;

            var date = DateFormatter.GenerateLogDate(dateFormat, logEvent.Date);
            var level = Enum.GetName(typeof(LogLevel), logEvent.Level);
            var message = logEvent.Message;

            return String.Format("[{0}] [{1}]: {2}", date, level, message);
        }

        /// <summary>
        /// Generate a new log content with a custom complete format
        /// </summary>
        /// <param name="logEvents"></param>
        /// <param name="dateFormat"></param>
        /// <param name="existedFileContent"></param>
        /// <returns></returns>
        internal static string FormatFullLog(List<LogEvent> logEvents, LogDateFormat dateFormat, string existedFileContent = "")
        {
            var innerContent = String.Empty;

            if (!String.IsNullOrEmpty(existedFileContent)) innerContent = String.Concat(existedFileContent, Environment.NewLine);

            foreach (var logEvent in logEvents)
            {
                innerContent = String.Concat("\t(", FormatLogMessage(logEvent, dateFormat), ");", Environment.NewLine);
            }

            var date = DateTime.Now.ToString("yy-MM-dd");
            var outContent = String.Format("EventsOfDate:({0})", date);

            return String.Concat(outContent, Environment.NewLine, "{", Environment.NewLine, innerContent, "}");
        }
    }
}
