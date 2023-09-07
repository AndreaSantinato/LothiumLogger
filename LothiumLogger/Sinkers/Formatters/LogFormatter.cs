// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;

namespace LothiumLogger.Sinkers.Formatters
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
        public static string FormatLogFile(string existingContent, string newContent)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(newContent)) return string.Empty;
            if (!string.IsNullOrEmpty(existingContent))
            {
                result = string.Concat(existingContent, Environment.NewLine, newContent);
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
		public static string FormatLogMessage(ILogEvent logEvent, LogDateFormatEnum dateFormat)
        {
            if (logEvent == null) return string.Empty;
            if (string.IsNullOrEmpty(logEvent.Message)) return string.Empty;

            var date = DateFormatter.GenerateLogDate(dateFormat, logEvent.Date);
            var level = Enum.GetName(typeof(LogLevelEnum), logEvent.Level);
            var message = logEvent.Message;

            return string.Format("[{0}] [{1}]: {2}", date, level, message);
        }

        /// <summary>
        /// Generate a new log content with a custom complete format
        /// </summary>
        /// <param name="logEvents"></param>
        /// <param name="dateFormat"></param>
        /// <param name="existedFileContent"></param>
        /// <returns></returns>
        public static string FormatFullLog(List<ILogEvent> logEvents, LogDateFormatEnum dateFormat, string existedFileContent = "")
        {
            var innerContent = string.Empty;

            if (!string.IsNullOrEmpty(existedFileContent)) innerContent = string.Concat(existedFileContent, Environment.NewLine);

            foreach (var logEvent in logEvents)
            {
                innerContent = string.Concat("\t(", FormatLogMessage(logEvent, dateFormat), ");", Environment.NewLine);
            }

            var date = DateTime.Now.ToString("yy-MM-dd");
            var outContent = string.Format("EventsOfDate:({0})", date);

            return string.Concat(outContent, Environment.NewLine, "{", Environment.NewLine, innerContent, "}");
        }
    }
}
