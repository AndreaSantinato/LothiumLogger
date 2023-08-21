// System Class
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
// Custom Class
using LothiumLogger.Enumerations;
using LothiumLogger.Interfaces.Sinks;
using LothiumLogger.Formatters;

namespace LothiumLogger.Sinks
{
    internal class FileSinks : IFileSinks
    {
        #region Dispose Safehandle Property

        /// <summary>
        /// Used to detect redundant calls
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion

        #region Class Property

        /// <summary>
        /// Define the type of the sink service
        /// </summary>
        internal Type SinkType => typeof(FileSinks);

        /// <summary>
        /// Define if the logger can use this sink
        /// </summary>
        internal bool EnableLogging { get; set; }

        /// <summary>
        /// Contains the file's name
        /// </summary>
        internal string FileName { get; set; }

        /// <summary>
        /// Contains the file's path
        /// </summary>
        internal string FilePath { get; set; }

        /// <summary>
        /// Indicates the minimum writing accepted level
        /// </summary>
        internal LogLevel MinimumLevel { get; set; }

        /// <summary>
        /// Indicates the only writing level accepted
        /// </summary>
        internal LogLevel RestrictedToLevel { get; set; }

        #endregion

        #region Class Constructor & Destructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="enableLogging">Define if the logger can write log inside the file</param>
        /// <param name="name">Define the file's name</param>
        /// <param name="path">Define the file's path</param>
        public FileSinks
        (
            bool enableLogging,
            string name,
            string path,
            LogLevel minimumLogLevel = LogLevel.Normal,
            LogLevel restrictedToLogLevel = LogLevel.Normal
        )
        {
            EnableLogging = enableLogging;
            FileName = name;
            FilePath = path;
            MinimumLevel = minimumLogLevel;
            RestrictedToLevel = restrictedToLogLevel;
        }

        /// <summary>
        /// Dispose the ConsoleLogger Instance
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
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        public bool Initialize(
            LoggerConfiguration configuration,
            LogEvent logEvent
        )
        {
            if (!EnableLogging) return false;
            if (configuration == null) return false;
            if (logEvent == null) return false;
            if (string.IsNullOrEmpty(logEvent.Message)) return false;
            return logEvent.Level >= MinimumLevel ? false : true;
        }

        /// <summary>
        /// Generate the output file's writing path
        /// </summary>
        /// <param name="name">Contains the name of the log file</param>
        /// <param name="path">Contains the path of the log file</param>
        /// <returns>A string with the full output directory based on the running operating system</returns>
        public string GenerateWritingPath(string name, string path)
        {
            // Inizialize the log path
            path = !string.IsNullOrEmpty(path) ? Path.Combine(path) : Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            if (!Directory.Exists(path))
            {
                // The directory doesn't exist, so it will create it
                Directory.CreateDirectory(path);
            }

            // Inizialize the log file
            if (string.IsNullOrEmpty(name))
            {
                name = "Log";
            }
            name = string.Format("{0}_{1}.{2}", name, DateTime.Now.ToString("yyMMdd"), "ltlog");

            // Return the initialized path (file's Path + file's Name)
            return Path.Combine(path, name);
        }

        /// <summary>
        /// Read the content of a log file
        /// </summary>
        /// <param name="filePath">Contains the path of the choosen file to read</param>
        /// <returns>A string with the content of the readed file</returns>
        public string Read(string path)
        {
            string? result = null;
            try
            {
                if (!File.Exists(path))
                {
                    // The file doesn't exist, so the method will return an empty string
                    throw new Exception("No path specified!!");
                }

                string content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content))
                {
                    // The file exist, but there is no content inside
                    throw new Exception("No content inside the file!!");
                }

                result = content.Trim();
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// Write a new log inside a choosen file
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        public void Write(LogEvent logEvent)
        {
            // Generate the writing output path for the file
            string outputPath = GenerateWritingPath(FilePath, FileName);

            // Read all the content from the file if it's exist
            string prevFileContent = Read(outputPath);

            // Generate the new content of the file
            string newFileContent = LogFormatter.FormatLogMessage(logEvent, LogDateFormat.Standard);

            // Using the previous content and the new content to generate the writing content
            string content = LogFormatter.FormatLogFile(prevFileContent, newFileContent);

            // Write the content inside the specific output path of the file
            File.WriteAllText(outputPath, content);
        }

        /// <summary>
        /// Write a new log list inside a choosen file
        /// </summary>
        /// <param name="logEvents">Contains a list of log event occured</param>
        public void Write(List<LogEvent> logEvents)
        {
            if (logEvents == null || logEvents.Count() == 0)
            {
                return;
            }
            foreach (LogEvent logEvent in logEvents)
            {
                Write(logEvent);
            }
        }

        #endregion
    }
}
