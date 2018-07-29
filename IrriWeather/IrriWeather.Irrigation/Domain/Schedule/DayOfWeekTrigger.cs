using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DayOfWeekTrigger : Trigger
    {

        public DayOfWeekTrigger(DaysOfWeek days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = TriggerType.DaysOfWeek;

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



        public DaysOfWeek Days { get; private set; }
        public TimeSpan StartTime { get; private set; }


        public override string BuildCronExpression()
        {
            return CronExpression.EverySpecificWeekDayAt(StartTime.Hours, StartTime.Minutes, Days);
        }
    }
}
