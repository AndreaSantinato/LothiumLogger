// System Class
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
// Core Class
using LothiumLogger.Interfaces;
using LothiumLogger.Enumerations;
using LothiumLogger.Sinkers.Formatters;
using LothiumLogger.Sinkers.ExtensionObjects;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Internal class for the writing of the log's events inside one or more files
    /// </summary>
    internal class FileSinker : ISinker
    {
        #region Private Class Property

        private SinkTypeEnum _sinkType { get; set; }
        private bool _enableLogging { get; set; }
        private string _fileName { get; set; }
        private string _filePath { get; set; }
        private LogLevelEnum _minimumLevel { get; set; }
        private LogLevelEnum _restrictedToLevel { get; set; }
        private LogFileTypeEnum _fileType { get; set; }

        #endregion

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

        #region Class Constructor & Destructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="enableLogging">Define if the logger can write log inside the file</param>
        /// <param name="name">Define the file's name</param>
        /// <param name="path">Define the file's path</param>
        /// <param name="minimumLogLevel">Define the minimum accepted logging level to write</param>
        /// <param name="restrictedToLogLevel">Define the only accepted logging level to write</param>
        /// <param name="fileType">Define the file's type to generate and write content into it</param>
        public FileSinker
        (
            bool enableLogging,
            string name,
            string path,
            LogLevelEnum minimumLogLevel = LogLevelEnum.Normal,
            LogLevelEnum restrictedToLogLevel = LogLevelEnum.Normal,
            LogFileTypeEnum fileType = LogFileTypeEnum.GenericLog
        )
        {
            _sinkType = SinkTypeEnum.File;
            _enableLogging = enableLogging;
            _fileName = name;
            _filePath = path;
            _minimumLevel = minimumLogLevel;
            _restrictedToLevel = restrictedToLogLevel;
            _fileType = fileType;
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
        /// Return the sink's type
        /// </summary>
        /// <returns>Sink's Type</returns>
        public SinkTypeEnum GetSinkType() => _sinkType;

        /// <summary>
        /// Define if the sink is enabled for logging events
        /// </summary>
        /// <returns>True Or False Status</returns>
        public bool IsSinkerEnabled() => _enableLogging;

        /// <summary>
        /// Return the minimum accepted log level accepted by the sink
        /// </summary>
        /// <returns>Minimum accepted log's level foreach log event</returns>
        public LogLevelEnum GetMinimumLogLevel() => _minimumLevel;

        /// <summary>
        /// Return the only accepted log level by the sink
        /// </summary>
        /// <returns>The only accepted log level foreach log event</returns>
        public LogLevelEnum GetRestrictedLogLevel() => _restrictedToLevel;

        /// <summary>
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        public bool Initialize(ILoggerConfiguration configuration, ILogEvent logEvent)
        {
            // Check if the sink can write the log
            if (!IsSinkerEnabled() || 
                configuration == null ||
                logEvent == null ||
                string.IsNullOrEmpty(logEvent.Message)
            ) return false;

            // Check if the log's restricted level allow the sink to write the log     
            if (GetRestrictedLogLevel() != LogLevelEnum.Normal && GetRestrictedLogLevel() != logEvent.Level) return false;

            // Check if the log's minimum level allow the sink to write the log
            return logEvent.Level >= GetMinimumLogLevel() ? true : false;
        }

        /// <summary>
        /// Generate the output file's writing path
        /// </summary>
        /// <param name="name">Contains the name of the log file</param>
        /// <param name="path">Contains the path of the log file</param>
        /// <returns>A string with the full output directory based on the running operating system</returns>
        public string GenerateWritingPath(string path, string name)
        {
            // Inizialize the log path
            path = !string.IsNullOrEmpty(path) ? Path.Combine(path) : Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            // if the directory doesn't exist will create it
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);


            // Generate the file's extension
            string fileExtension = String.Empty;
            switch (_fileType)
            {
                case LogFileTypeEnum.GenericLog:
                    fileExtension = "log";
                    break;
                case LogFileTypeEnum.LothiumLog:
                    fileExtension = "ltlog";
                    break;
            }

            // Inizialize the log file
            if (string.IsNullOrEmpty(name)) name = "Log";
            name = string.Format("{0}_{1}.{2}", name, DateTime.Now.ToString("yyMMdd"), fileExtension);

            // Return the initialized path (file's Path + file's Name)
            return Path.Combine(path, name);
        }

        /// <summary>
        /// Read the content of a log file
        /// </summary>
        /// <param name="path">Contains the path of the choosen file to read</param>
        /// <returns>A string with the content of the readed file</returns>
        public string Read(string path)
        {
            string? result = null;

            try
            {
                // Verify if the file exist
                if (!File.Exists(path)) throw new FileNotFoundException("No path specified!!");

                // Read and process the file's content
                string content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content)) throw new Exception("No content inside the file!!");
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
        public void Write(ILogEvent logEvent)
        {
            // Generate the writing output path for the file
            string outputPath = GenerateWritingPath(_filePath, _fileName);

            // Generate the file content with all the log events based on the choosen file's type
            string content = String.Empty;
            switch (_fileType)
            {
                case LogFileTypeEnum.GenericLog:
                    // 1) Read all the content from the file if it's exist
                    // 2) Generate the new content of the file
                    // 3) Using the previous content and the new content to generate the writing content
                    content = LogFormatter.FormatLogFile(
                        Read(outputPath),
                        LogFormatter.FormatLogMessage(logEvent, LogDateFormatEnum.Standard)
                    );
                    break;
                case LogFileTypeEnum.LothiumLog:
                    // 1) Create a new instance of a Lothium Log File
                    // 2) Populate the object log's events with all previously written log's events
                    // 3) Generate the new content
                    LothiumLogFile lothiumLogFile = new LothiumLogFile(_fileName, DateTimeOffset.Now);
                    lothiumLogFile.LoadFileFromPath(outputPath);
                    lothiumLogFile.AddNewLogEvent(logEvent);
                    content = lothiumLogFile.GenerateFileContent();
                    break;
            }

            // Write the content inside the specific output path of the file
            File.WriteAllText(outputPath, content);
        }

        #endregion
    }
}
