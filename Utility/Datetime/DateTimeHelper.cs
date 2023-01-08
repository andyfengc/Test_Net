using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Datetime
{
    public class DateTimeHelper
    {
        public static DateTime GetThisWeekDay(DateTime start, DayOfWeek day)
        {
            int daysToAdd = (int) day - (int) start.DayOfWeek;
            return start.AddDays(daysToAdd);
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public static bool IsWeekendHours(DateTime time)
        {
            int Off_Hours = 17;
            if (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            if (time.DayOfWeek == DayOfWeek.Friday)
            {
                if (time.Hour >= Off_Hours)
                {
                    return true;
                }
            }
            return false;
        }

        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            int numberDays = 0;

            while (numberDays != days)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    numberDays++;
                }
            };

            return date;
        }

        public static int? GetBusinessBaysBetween(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return null;
            }
            var firstDay = startDate.Value;
            var lastDay = endDate.Value;
            if (firstDay > lastDay)
                return 0;

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            return businessDays;
        }
    }
}
