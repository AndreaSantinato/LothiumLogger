// System Class
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;
// Core Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces;
using LothiumLogger.Sinkers.Formatters;

namespace LothiumLogger.Sinkers.ExtensionObjects
{
    /// <summary>
    /// Defines a Lothium Log File
    /// Contains the name and the date of the generated file and all the log events to be written inside of it
    /// </summary>
    internal class LothiumLogFile : IDisposable
    {
        #region Class Property

        private bool _disposedValue;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private string _fileName { get; set; }
        private DateTimeOffset _fileDate { get; set; }
        private List<string> _logEvents { get; set; }

        #endregion

        #region Class Constructor & Destructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name">Defines the name for the lothium log file</param>
        /// <param name="date">Defines the date for the lothium log file</param>
        public LothiumLogFile(string name, DateTimeOffset date)
        {
            _fileName = name;
            _fileDate = date;
            _logEvents = new List<string>();
        }

        /// <summary>
        /// Dispose this Instance
        /// </summary>
        public void Dispose()
        {
            if (!_disposedValue)
            {
                _safeHandle.Dispose();
                _disposedValue = true;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Add new log's event to the event list
        /// </summary>
        /// <param name="logEvent">Contains the log event</param>
        public void AddNewLogEvent(ILogEvent logEvent)
        {
            if (logEvent == null) return;
            _logEvents.Add(LogFormatter.FormatLogMessage(logEvent, LogDateFormatEnum.Full));
        }

        /// <summary>
        /// Populate a LothiumLogFile from an existing file
        /// </summary>
        /// <param name="path"></param>
        public void LoadFileFromPath(string path)
        {
            // Check if the path is valorized and if the file actually exist
            if (string.IsNullOrEmpty(path)) return;
            if (!File.Exists(path)) return;

            // Read all the content from the file
            foreach (Match match in Regex.Matches(File.ReadAllText(path), "{[^}]+}"))
            {
                var content = match.Value;
                content = content.Replace("{", string.Empty);
                content = content.Replace("}", string.Empty);
                content = content.Replace("\t", string.Empty);
                content = content.Replace(Environment.NewLine, string.Empty);
                _logEvents.Add(content);
            }
        }

        /// <summary>
        /// Generate a new lothium log file (.ltlog) from this object instance
        /// </summary>
        /// <returns></returns>
        public string GenerateFileContent()
        {
            // Generate the inner content
            string innerContent = string.Empty;
            foreach (var logEvent in _logEvents)
            {
                innerContent = string.Concat(
                    innerContent,
                    "\t",
                    logEvent,
                    "",
                    Environment.NewLine
                );
            }
            // Generate the final content
            string content = string.Concat(
                string.Format("EventsOfDate:({0})", _fileDate.ToString("yyyy/MM/dd")),
                Environment.NewLine,
                "{",
                Environment.NewLine,
                innerContent,
                "};"
            );

            // Return the generated result
            return content;
        }

        #endregion
    }
}
