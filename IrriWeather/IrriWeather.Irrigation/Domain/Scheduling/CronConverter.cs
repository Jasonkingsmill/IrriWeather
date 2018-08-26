using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class CronConverter
    {
        public static string ToCronRepresentationSingle(DaysOfWeek day)
        {
            switch (day)
            {
                case DaysOfWeek.Monday:
                    return "MON";
                case DaysOfWeek.Tuesday:
                    return "TUE";
                case DaysOfWeek.Wednesday:
                    return "WED";
                case DaysOfWeek.Thursday:
                    return "THU";
                case DaysOfWeek.Friday:
                    return "FRI";
                case DaysOfWeek.Saturday:
                    return "SAT";
                case DaysOfWeek.Sunday:
                    return "SUN";
                default:
                    throw new ArgumentException();
            }
        }

        public static string ToCronRepresentationSingle(int day)
        {
            switch (day)
            {
                case 1:
                    return "MON";
                case 2:
                    return "TUE";
                case 3:
                    return "WED";
                case 4:
                    return "THU";
                case 5:
                    return "FRI";
                case 6:
                    return "SAT";
                case 7:
                    return "SUN";
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Converts enumerator DaysOfWeek into string representation
        /// like "MON, TUE, WED"
        /// </summary>
        /// <param name="days">Enumerator to convert</param>
        /// <returns>String representation</returns>
        public static string ToCronRepresentation(DaysOfWeek days)
        {
            return String.Join(",", GetFlags(days).Select(ToCronRepresentationSingle));
        }

        public static string ToCronRepresentation(IEnumerable<int> days)
        {
            return String.Join(",", days);
        }

        public static string ToCronWeekdayRepresentation(IEnumerable<int> days)
        {
            return String.Join(",", days.Select(x => ToCronRepresentationSingle(x)));
        }

        public static IEnumerable<DaysOfWeek> GetFlags(DaysOfWeek days)
        {
            return Enum.GetValues(days.GetType()).Cast<DaysOfWeek>().Where(v => days.HasFlag(v));
        }
    }
}
