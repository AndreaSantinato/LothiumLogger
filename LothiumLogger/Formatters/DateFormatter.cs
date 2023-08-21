// System Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Formatters
{
    /// <summary>
    /// Date Formatter Class For The Log Event's Date
    /// </summary>
    internal static class DateFormatter
    {
        /// <summary>
        /// Return the year from a date of a log event
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>An Int with the year value</returns>
        internal static int GetYearFromDate(DateTimeOffset date)
        {
            return date.Year;
        }

        /// <summary>
        /// Return the month from a date of a log event
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>An Int with the month value</returns>
        internal static int GetMonthFromDate(DateTimeOffset date)
        {
            return date.Month;
        }

        /// <summary>
        /// Retrive the name of a month from a date
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>A String with the month's name</returns>
        internal static string GetMonthNameFromDate(DateTimeOffset date)
        {
            {
                string monthName = String.Empty;
                switch (GetMonthFromDate(date))
                {
                    #region Genuary

                    case 1:
                        monthName = "Gen";
                        break;

                    #endregion
                    #region February

                    case 2:
                        monthName = "Feb";
                        break;

                    #endregion
                    #region March

                    case 3:
                        monthName = "Mar";
                        break;

                    #endregion
                    #region April

                    case 4:
                        monthName = "Apr";
                        break;

                    #endregion
                    #region May

                    case 5:
                        monthName = "May";
                        break;

                    #endregion
                    #region June

                    case 6:
                        monthName = "Jun";
                        break;

                    #endregion
                    #region July

                    case 7:
                        monthName = "Jul";
                        break;

                    #endregion
                    #region August

                    case 8:
                        monthName = "Aug";
                        break;

                    #endregion
                    #region September

                    case 9:
                        monthName = "Sep";
                        break;

                    #endregion
                    #region October

                    case 10:
                        monthName = "Oct";
                        break;

                    #endregion
                    #region November

                    case 11:
                        monthName = "Nov";
                        break;

                    #endregion
                    #region December

                    case 12:
                        monthName = "Dec";
                        break;

                        #endregion
                }
                return monthName;
            }
        }

        /// <summary>
        /// Return the day from a date of a log event
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>An Int with the day value</returns>
        internal static int GetDayFromDate(DateTimeOffset date)
        {
            return date.Day;
        }

        /// <summary>
        /// Format the date that will be used for the log based on a choosen format's type
        /// </summary>
        /// <param name="dateFormat">Choosen Date Format</param>
        /// <returns>A String formatted based on the passed Date Fornat</returns>
        internal static string GenerateLogDate(LogDateFormat dateFormat, DateTimeOffset date)
        {
            string result = String.Empty;

            switch (dateFormat)
            {
                case LogDateFormat.Minimal:
                    result = date.ToString("yyyymmdd");
                    break;
                case LogDateFormat.Standard:
                    result = date.ToString("yyyy/mm/dd hh:mm:ss");
                    break;
                case LogDateFormat.Full:
                    var year = GetYearFromDate(date);
                    var monthName = GetMonthNameFromDate(date);
                    var day = GetDayFromDate(date);
                    result = String.Format("({0}) {1} {2} {3}", year, monthName, day, date.ToString("hh:mm:ss"));
                    break;
            }

            return result;
        }
    }
}
