// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces.Sinks
{
    internal interface IFileSinks : IDisposable
    {
        /// <summary>
        /// Initialize the writing of a new log event inside the Console
        /// </summary>
        /// <param name="configuration">Contains the current logger configuration</param>
        /// <param name="logEvent">Contains the log event occured</param>
        /// <returns>True/False Status Value</returns>
        bool Initialize(LoggerConfiguration configuration, LogEvent logEvent);

        /// <summary>
        /// Generate the output file's writing path
        /// </summary>
        /// <param name="name">Contains the name of the log file</param>
        /// <param name="path">Contains the path of the log file</param>
        /// <returns>A string with the full output directory based on the running operating system</returns>
        string GenerateWritingPath(string name, string path);

        /// <summary>
        /// Read the content of a log file
        /// </summary>
        /// <param name="filePath">Contains the path of the choosen file to read</param>
        /// <returns>A string with the content of the readed file</returns>
        string Read(string filePath);

        /// <summary>
        /// Write a new log inside a choosen file
        /// </summary>
        /// <param name="logEvent">Contains the log event occured</param>
        void Write(LogEvent logEvent);

        /// <summary>
        /// Write a new log list inside a choosen file
        /// </summary>
        /// <param name="logEvents">Contains a list of log event occured</param>
        void Write(List<LogEvent> logEvents);
    }
}
