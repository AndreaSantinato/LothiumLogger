// System Class
using System;
// Custom Class
using LothiumLogger.Enumerations;

namespace LothiumLogger.Sinkers.Formatters
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
        public static int GetYearFromDate(DateTimeOffset date) => date.Year;

        /// <summary>
        /// Return the month from a date of a log event
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>An Int with the month value</returns>
        public static int GetMonthFromDate(DateTimeOffset date) => date.Month;

        /// <summary>
        /// Retrive the name of a month from a date
        /// </summary>
        /// <param name="date">Contains the date of a log event</param>
        /// <returns>A String with the month's name</returns>
        public static string GetMonthNameFromDate(DateTimeOffset date)
        {
            {
                string monthName = string.Empty;
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
        public static int GetDayFromDate(DateTimeOffset date) => date.Day;

        /// <summary>
        /// Format the date that will be used for the log based on a choosen format's type
        /// </summary>
        /// <param name="dateFormat">Choosen Date Format</param>
        /// <returns>A String formatted based on the passed Date Fornat</returns>
        public static string GenerateLogDate(LogDateFormatEnum dateFormat, DateTimeOffset date)
        {
            string result = string.Empty;

            switch (dateFormat)
            {
                case LogDateFormatEnum.Minimal:
                    result = date.ToString("yyyymmdd");
                    break;
                case LogDateFormatEnum.Standard:
                    result = date.ToString("yyyy/mm/dd hh:mm:ss");
                    break;
                case LogDateFormatEnum.Full:
                    var year = GetYearFromDate(date);
                    var monthName = GetMonthNameFromDate(date);
                    var day = GetDayFromDate(date);
                    result = string.Format("({0}) {1} {2} {3}", year, monthName, day, date.ToString("hh:mm:ss"));
                    break;
            }

            return result;
        }
    }
}
