// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger
{
    public class LogEventObject
    {
        public DateTime EventDate { get; set; }

        public LogLevel EventLevel { get; set; }

        public string? EventMessage { get; set; }

        public LogEventObject()
        {
            EventDate = DateTime.Now;
            EventLevel = LogLevel.Normal;
            EventMessage = string.Empty;
        }

        public LogEventObject(DateTime eventDate, LogLevel eventLevel, string? eventMessage)
        {
            EventDate = eventDate;
            EventLevel = eventLevel;
            EventMessage = eventMessage;
        }
    }
}
