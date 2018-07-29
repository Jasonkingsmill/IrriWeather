using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class OddDaysTrigger : Trigger
    {
        private OddDaysTrigger() { }

        public OddDaysTrigger(TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = TriggerType.EvenDays;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException("Time must be less than 24 hours", nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));

            StartTime = startTime;
            Duration = duration;
        }

        public TimeSpan StartTime { get; private set; }

        public override string BuildCronExpression()
        {
            return CronExpression.EverySpecificDaysEveryNMonthAt(new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31 }, 1, StartTime.Hours, StartTime.Minutes);
        }
    }
}
