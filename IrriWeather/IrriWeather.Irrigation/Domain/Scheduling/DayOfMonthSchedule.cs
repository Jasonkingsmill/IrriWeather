using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class DayOfMonthSchedule : Schedule
    {
        private string _days = "";
        private HashSet<int> days = new HashSet<int>();

        private DayOfMonthSchedule() { }

        private DayOfMonthSchedule(TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = ScheduleType.DaysOfMonth;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException("Time must be less than 24 hours", nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));

            StartTime = startTime;
            Duration = duration;
        }

        public DayOfMonthSchedule(int day, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : this(startTime, duration, enabledUntil, isEnabled)
        {
            if (day > 31 || day < 1)
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
            days.Add(day);
        }

        public DayOfMonthSchedule(IEnumerable<int> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : this(startTime, duration, enabledUntil, isEnabled)
        {
            foreach (var day in days)
            {
                if (day > 31 || day < 1)
                    throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
                this.days.Add(day);
            }
        }

        public IEnumerable<int> Days { get => _days.Split(";").Select(x=>int.Parse(x)); set => this._days = string.Join(";", value); }
        public TimeSpan StartTime { get; private set; }

        public override string BuildCronExpression()
        {
            return CronExpression.EverySpecificDaysEveryNMonthAt(Days, 1, StartTime.Hours, StartTime.Minutes);
        }
    }
}
