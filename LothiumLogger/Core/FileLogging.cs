// System Class
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Core
{
    /// <summary>
    /// Class Dedicated To The File Logging Methods
    /// </summary>
    internal sealed class FileLogging
	{
        /// <summary>
        /// Read the content of an existing log file
        /// </summary>
        /// <param name="path">Indicates the path where the file is located</param>
        /// <param name="logFormat">Indicates the format of the file</param>
        /// <returns>The Readed File Content</returns>
        internal static string ReadFromFile(string path, LogFormat logFormat)
        {
            string result = String.Empty;

            if (!File.Exists(path)) return result;

            string fileContent = File.ReadAllText(path);
            if (!String.IsNullOrEmpty(fileContent))
            {
                switch (logFormat)
                {
                    case LogFormat.Easy:
                        result = fileContent.Trim();
                        break;
                    case LogFormat.Full:
                        foreach (Match match in Regex.Matches(fileContent, "{[^}]+}"))
                        {
                            result = String.Concat(Environment.NewLine, match.Value);
                            result = result.Replace("{", String.Empty);
                            result = result.Replace("}", String.Empty);
                            result = result.Replace(String.Format("{0}{1}", Environment.NewLine, Environment.NewLine), String.Empty);
                            result.Trim();
                        }
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Wreite a new log file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        internal static void WriteToFile(LogEventObject logEvent, string fileName, string filePath)
		{
            string outputPath = FileLogging.InitializeWriting(filePath, fileName);
            string prevContent = FileLogging.ReadFromFile(outputPath, LogFormat.Easy);
            string newContent = FormatManager.FormatLogMessage(logEvent, LogDateFormat.Standard);
            string content = FormatManager.FormatLogFile(prevContent, newContent);

            File.WriteAllText(outputPath, content);
        }

        /// <summary>
        /// Generate the Output full writing filestream path
        /// </summary>
        /// <param name="filePath">Contains the output file's path</param>
        /// <param name="fileName">Contains the output file's name</param>
        /// <returns>The Full Combined Path</returns>
        internal static string InitializeWriting(string filePath, string fileName)
        {
            // Inizialize the log path
            if (String.IsNullOrEmpty (filePath)) 
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            }
            else
            {
                filePath = Path.Combine(filePath);
            }
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            // Inizialize the log file
            if (String.IsNullOrEmpty(fileName))
            {
                fileName = "Log";
            }
            fileName = String.Format("{0}_{1}.{2}", fileName, DateTime.Now.ToString("yyMMdd"), "ltlog");

            // Return the initialized path (FilePath + FileName)
            return Path.Combine(filePath, fileName);
        }
	}
}

