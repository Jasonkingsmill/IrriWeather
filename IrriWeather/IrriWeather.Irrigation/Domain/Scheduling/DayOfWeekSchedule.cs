using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class DayOfWeekSchedule : Schedule
    {
        private DayOfWeekSchedule() { }

        private string _days = "";
        public DayOfWeekSchedule(IEnumerable<string> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = ScheduleType.DaysOfWeek;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException("Time must be less than 24 hours", nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));

            StartTime = startTime;
            Duration = duration;
            Days = days;
        }



        public IEnumerable<string> Days { get => _days.Split(";").Select(x => x); set => this._days = string.Join(";", value); }
        public TimeSpan StartTime { get; private set; }


        public override string BuildCronExpression()
        {
            return CronExpression.EverySpecificWeekDayAt(StartTime.Hours, StartTime.Minutes, Days);
        }
    }
}
