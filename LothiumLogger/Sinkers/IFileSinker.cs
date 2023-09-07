// System Class
using System;

namespace LothiumLogger.Sinkers
{
    /// <summary>
    /// Interface that define a FileSink
    /// </summary>
    internal interface IFileSinker : ISinker
    {
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
    }
}
