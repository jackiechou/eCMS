using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Eagle.Common.Utilities
{
    public static class DateTimeUtils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime? info, string format)
        {
            try
            {
                if (info.HasValue)
                {
                    return info.Value.ToString(format);
                }
                return String.Empty;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTimeInfo"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsDateTime(string dateTimeInfo, string format)
        {
            try
            {
                DateTime.ParseExact(dateTimeInfo, format, new System.Globalization.DateTimeFormatInfo());
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string info, string format)
        {
            try
            {
                return DateTime.ParseExact(info, format, new DateTimeFormatInfo());
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(DateTime info, string format)
        {
            try
            {
                if (info == null)
                {
                    return String.Empty;
                }
                return info.ToString(format);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return String.Empty;
            }
        }
        public static string[] datetime_formats = {
                           "M/d/yyyy", "M/d/yyyy h:mm:ss","M/d/yyyy h:mm:ss tt","M/d/yyyy h:mm tt","M/d/yyyy hh:mm tt","M/d/yyyy hh tt","M/d/yyyy h:mm","M/d/yy",                            
                           "MM/dd/yyyy", "MM/dd/yyyy hh:mm:ss", "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",                            
                           "dd/MM/yyyy","dd-MMM-yy","dd-MMM-yyyy","dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss","dd/MM/yyyy hh:mm",                           
                           "yy/MM/dd","yyyy/MM/dd","yyyy/MM/dd hh:mm:ss tt","yyyy-MM-dd","yyyy-MM-dd HH:mm:ss"};

        //public static string[] datetime_formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
        //           "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
        //           "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
        //           "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
        //           "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        //public static string FormatDateTimeString(string strInput, string strCustomFormat)
        //{
        //    string strFormattedDateTime = string.Empty;
        //    if(strInput.Length >=8)
        //    {
        //        DateTime SELECTED_DATE;
        //        if (DateTime.TryParseExact(strInput, datetime_formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
        //            strFormattedDateTime = SELECTED_DATE.ToString(strCustomFormat);
        //        }                    
        //        else
        //        {
        //            SELECTED_DATE = Convert.ToDateTime(strInput);
        //            strFormattedDateTime = SELECTED_DATE.ToString(strCustomFormat);
        //        }   
        //    }
        //    return strFormattedDateTime;
        //}

        public static string FormatDateTimeString(string strInput, string strCustomFormat)
        {
            string strFormattedDateTime = string.Empty;
            if (strInput.Length >= 8)
            {
                DateTime SELECTED_DATE;
                if (DateTime.TryParseExact(strInput, strCustomFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                    strFormattedDateTime = SELECTED_DATE.ToString(strCustomFormat);
                else
                {
                    SELECTED_DATE = Convert.ToDateTime(strInput);
                    strFormattedDateTime = SELECTED_DATE.ToString(strCustomFormat);
                }
            }
            return strFormattedDateTime;
        }
        public static DateTime ParseDate(string strDateTimeInput)
        {
            DateTime FormattedDateTime = new DateTime();
            if (strDateTimeInput.Length >= 8)
            {
                DateTime SELECTED_DATE;
                if (DateTime.TryParseExact(strDateTimeInput, datetime_formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out SELECTED_DATE))
                    FormattedDateTime = SELECTED_DATE;
                else
                    FormattedDateTime = Convert.ToDateTime(strDateTimeInput);               
            }          
            return FormattedDateTime;
        }      

        /// <summary>
        /// Parse DateTime To DimDate
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ConvertDateTimeToDimDate(DateTime obj)
        {
            try
            {
                int? result = null;
                if (IsDateTime(obj) == true)
                {
                    result = Convert.ToInt32(DateTime.Parse(obj.ToString()).ToString("yyyyMMdd"));
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertDimDateToDateTime(object obj)
        {
            try
            {
                DateTime? result = null;
                if (IsInt(obj) == true)
                {
                    string strDateTime = obj.ToString();
                    result = new DateTime(ParseInt(strDateTime.Substring(0, 4)), ParseInt(strDateTime.Substring(4, 2)), ParseInt(strDateTime.Substring(6, 2)));
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool TryConvertToDate(string inputDate, out DateTime? outputDate)
        {

            try
            {
                string[] arrayDate = inputDate.Split('/');
                outputDate = new DateTime(int.Parse(arrayDate[2]), int.Parse(arrayDate[1]), int.Parse(arrayDate[0]));
                return true;
            }
            catch// (Exception ex)
            {
                outputDate = null;
                return false;
            }
        }

        #region Validate DateTime ====================================================
        /// <summary>
        /// Parse Object To Integer Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ParseInt(object obj)
        {
            try
            {
                return Int32.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Validate Integer Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInt(object obj)
        {
            try
            {
                Int32.Parse(obj.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validate DateTime Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDateTime(object obj)
        {
            try
            {
                DateTime.Parse(obj.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion Validate DateTime =================================================

        public static int FirstDayOfPredefinedPreviousMonth(int PredefinedPreviousMonth)
        {
            DateTime firstDayOfPredefinedPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-System.Math.Abs(PredefinedPreviousMonth));
            return firstDayOfPredefinedPreviousMonth.Day;
        }

        public static int LastDayOfPredefinedPreviousMonth(int PredefinedPreviousMonth)
        {
            DateTime lastDayOfPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month - PredefinedPreviousMonth, 1).AddDays(-1);
            return lastDayOfPreviousMonth.Day;
        }

        public static DateTime FirstDateOfPredefinedPreviousMonth(int PredefinedPreviousMonth)
        {
            DateTime firstDateOfPredefinedPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-System.Math.Abs(PredefinedPreviousMonth));
            return firstDateOfPredefinedPreviousMonth;
        }

        public static DateTime LastDateOfPredefinedPreviousMonth(int PredefinedPreviousMonth)
        {
            DateTime lastDateOfPredefinedPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-System.Math.Abs(PredefinedPreviousMonth));
            return lastDateOfPredefinedPreviousMonth;
        }

        public static int FirstDayOfPreviousMonth()
        {
            DateTime firstDayOfPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            return firstDayOfPreviousMonth.Day;
        }

        public static DateTime FirstDateOfPreviousMonth()
        {
            DateTime firstDayOfPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            return firstDayOfPreviousMonth;
        }

        public static int LastDayOfPreviousMonth()
        {
            DateTime lastDayOfPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            return lastDayOfPreviousMonth.Day;
        }

        public static DateTime LastDateOfPreviousMonth()
        {
            DateTime lastDateOfPreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            return lastDateOfPreviousMonth;
        }

        public static int DaysInMonthOfSelectedDate(DateTime dateTime)
        {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return daysInMonth;
        }

        public static int FirstDayOfCurrentMonth()
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return firstDayOfMonth.Day;
        }

        public static DateTime FirstDateOfCurrentMonth()
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return firstDateOfMonth;
        }

        public static int LastDayOfCurrentMonth()
        {
            DateTime firstDayOfCurrentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime lastDayOfCurrentMonth = firstDayOfCurrentMonth.AddMonths(1).AddSeconds(-1);          
            return lastDayOfCurrentMonth.Day;         
        }

        public static DateTime LastDateOfCurrentMonth()
        {
            DateTime firstDateOfCurrentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime lastDateOfCurrentMonth = firstDateOfCurrentMonth.AddMonths(1).AddSeconds(-1);
            return lastDateOfCurrentMonth;
        }

        public static int FirstDayOfCurrentWeek()
        {
            return DateTime.Now.AddDays(1-(int)DateTime.Now.DayOfWeek).Day;
        }

        public static DateTime FirstDateOfCurrentWeek()
        {
            return DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek);
        }

        public static int LastDayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek)).Day;
        }

        public static DateTime LastDateOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime TuesdayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(2 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime WednesdayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(3 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime ThursdayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(4 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime LastFridayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(5 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime LastSarturdayOfCurrentWeek()
        {
            return (DateTime.Now.AddDays(6 - (int)DateTime.Now.DayOfWeek));
        }

        public static DateTime LastSundayOfCurrentWeek()
        {          
            return (DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek));
        }
    }
}
