﻿using System;
using LothiumLogger.Enumerations;

namespace LothiumLogger.Interfaces
{
    public interface IWriteLogToConfiguration
    {
        /// <summary>
        /// Generate a new Logging Writing Configuration specific for the Console Logging
        /// </summary>
        /// <param name="restrictedToLevel">Indicates the minimum logging level to be used</param>
        /// <returns>A new Logger Writing Configurations</returns>
        IWriteLogToConfiguration Console(LogLevel minimumLevel = LogLevel.Normal, LogLevel restrictedToLevel = LogLevel.Normal);

        /// <summary>
        /// Generate a new Logging Writing Configuration specific for the File Logging
        /// </summary>
        /// <param name="fileName">Indicates the name of the log file that will be created</param>
        /// <param name="filePath">Indicates the path where the log file will be created</param>
        /// <param name="restrictedToLevel">Indicates the minimum logging level to be used</param>
        /// <returns>A new Logger Writing Configurations</returns>
        IWriteLogToConfiguration File(string fileName, string filePath, LogLevel minimumLevel = LogLevel.Normal, LogLevel restrictedToLevel = LogLevel.Normal);
    }
}
