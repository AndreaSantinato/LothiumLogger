// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces.Configurations
{
    public interface ISinkConfiguration
    {
        /// <summary>
        /// Generate a new Logging Writing Configuration specific for the Console Logging
        /// </summary>
        /// <param name="minimumLevel">Indicates the minimum logging level to accept</param>
        /// <param name="restrictedToLevel">Indicates the restricted logging level to accept</param>
        /// <returns>A new Logger Writing Configurations</returns>
        ISinkConfiguration Console
        (
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        );

        /// <summary>
        /// Generate a new Logging Writing Configuration specific for the File Logging
        /// </summary>
        /// <param name="fileName">Indicates the name of the log file that will be created</param>
        /// <param name="filePath">Indicates the path where the log file will be created</param>
        /// <param name="minimumLevel">Indicates the minimum logging level to accept</param>
        /// <param name="restrictedToLevel">Indicates the restricted logging level to accept</param>
        /// <returns>A new Logger Writing Configurations</returns>
        ISinkConfiguration File
        (
            string fileName, 
            string filePath,
            LogLevel minimumLevel = LogLevel.Normal,
            LogLevel restrictedToLevel = LogLevel.Normal
        );
    }
}

