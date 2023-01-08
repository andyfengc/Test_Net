using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DateHelper
    {
        /// <summary>
        /// To the UTC with timezone. e.g. 2016-05-04T04:09:21-04:00
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String</returns>
        public static string ToUtcWithTimezone(DateTime dateTime)
        {
            return string.Format("{0:yyyy-MM-ddThh:mm:ss}{1}",
                dateTime.ToLocalTime(),
                dateTime.ToString("%K"));
        }

        /// <summary>
        /// To the UTC string. e.g. 2016-05-04T16:09:21Z
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String</returns>
        public static string ToUtc(DateTime dateTime)
        {
            return dateTime.ToString("s") + "Z";
        }

        public static DateTime ToDateTime(DateTime time, double diffHours)
        {
            return time.AddHours(diffHours);
        }

        /// <summary>
        /// To the date time from UTC with timezone. e.g. 2016-08-03T12:41:25-07:00
        /// </summary>
        /// <param name="utcStr">The UTC string.</param>
        /// <returns>DateTime.</returns>
        public static DateTime ToDateTimeFromUtcWithTimezone(string utcStr)
        {
            return DateTime.Parse(utcStr);
        }

        /// <summary>
        /// To the date time from utc string. e.g. 2016-08-03T12:41:20.0904778Z
        /// </summary>
        /// <param name="utcStr">The UTC string.</param>
        /// <returns>DateTime.</returns>
        public static DateTime ToDateTimeFromUtc(string utcStr)
        {
            return DateTime.ParseExact(utcStr, "o", CultureInfo.InvariantCulture, DateTimeStyles.None); ;
        }

        public static bool IsBusinessHours(DateTime dateTime, int startHour = 6, int endHour = 22)
        {
            if (startHour > endHour)
            {
                throw new ArgumentException(string.Format("endHour:{0} should be greater than startHour:{1}", endHour, startHour));
            }
            var hour = dateTime.Hour;
            return hour >= startHour && hour < endHour;
        }


    }
}
