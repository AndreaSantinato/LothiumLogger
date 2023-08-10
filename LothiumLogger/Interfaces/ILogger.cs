// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LothiumLogger.Interfaces
{
    internal interface ILogger
    {
        /// <summary>
        /// Write a generic log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Write(string message);

        /// <summary>
        /// Write a generic log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Write(string message, object obj);

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Debug(string message);

        /// <summary>
        /// Write a debug log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Debug(string message, object obj);

        /// <summary>
        /// Write an information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Information(string message);

        /// <summary>
        /// Write a information log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Information(string message, object obj);

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Warning(string message);

        /// <summary>
        /// Write a warning log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Warning(string message, object obj);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Error(string message);

        /// <summary>
        /// Write a error log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Error(string message, object obj);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        public void Fatal(string message);

        /// <summary>
        /// Write a fatal log
        /// </summary>
        /// <param name="message">Contains the log message</param>
        /// <param name="obj">Contains the object to be serialized into the log</param>
        public void Fatal(string message, object obj);
    }
}
