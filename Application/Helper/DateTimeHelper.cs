using System;

namespace Application.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ToUserTimeZone(this DateTime dateTime, int timeDifferenceInMinutes)
        {
            // timeDifferenceInMinutes
            return dateTime.AddMinutes(-timeDifferenceInMinutes);
        }

        public static DateTime FromUserToUniversalTimeZone(this DateTime dateTime, int timeDifferenceInMinutes)
        {
            // timeDifferenceInMinutes
            return dateTime.AddMinutes(timeDifferenceInMinutes);
        }

        public static DateTime ToDateTime(this TimeSpan timeSpan, int dayOfWeek, DateTime dateTime)
        {
            if (dateTime.DayOfWeek.GetHashCode() == dayOfWeek)
            {
                return dateTime.Date.AddMinutes(Convert.ToInt32(timeSpan.TotalMinutes));
            }
            else if (dateTime.AddDays(1).DayOfWeek.GetHashCode() == dayOfWeek)
            {
                return dateTime.AddDays(1).Date.AddMinutes(Convert.ToInt32(timeSpan.TotalMinutes));
            }
            else if (dateTime.AddDays(-1).DayOfWeek.GetHashCode() == dayOfWeek)
            {
                return dateTime.AddDays(-1).Date.AddMinutes(Convert.ToInt32(timeSpan.TotalMinutes));
            }
            return dateTime;
        }
    }
}