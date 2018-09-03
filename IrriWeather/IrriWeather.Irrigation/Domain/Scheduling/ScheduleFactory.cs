using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class ScheduleFactory
    {
        public ScheduleFactory()
        {
        }

        internal Schedule CreateDateTimeSchedule(string name, string description, DateTime startDate, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            return new Schedule(ScheduleType.DateTime, name, description, null, startDate, startTime, duration, enabledUntil, isEnabled);
        }

        internal Schedule CreateDayOfMonthSchedule(string name, string description, DateTime startDate, IEnumerable<int> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            return new Schedule(ScheduleType.DaysOfMonth, name, description, days, startDate, startTime, duration, enabledUntil, isEnabled);
        }

        internal Schedule CreateDayOfWeekSchedule(string name, string description, DateTime startDate, IEnumerable<int> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            return new Schedule(ScheduleType.DaysOfWeek, name, description, days, startDate, startTime, duration, enabledUntil, isEnabled);
        }

        internal Schedule CreateEvenDaysSchedule(string name, string description, DateTime startDate, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            return new Schedule(ScheduleType.EvenDays, name, description, null, startDate, startTime, duration, enabledUntil, isEnabled);
        }

        internal Schedule CreateOddDaysSchedule(string name, string description, DateTime startDate, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            return new Schedule(ScheduleType.OddDays, name, description, null, startDate, startTime, duration, enabledUntil, isEnabled);
        }
    }
}
