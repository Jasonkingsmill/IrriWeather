using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class EvenDaysSchedule : Schedule
    {
        private EvenDaysSchedule() { }

        public EvenDaysSchedule(TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = ScheduleType.EvenDays;

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
            return CronExpression.EverySpecificDaysEveryNMonthAt(new int[] { 2,4,6,8,10,12,14,16,18,20,22,24,26,28,30}, 1, StartTime.Hours, StartTime.Minutes);
        }
    }
}
