using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class EvenDaysTrigger : Trigger
    {
        private EvenDaysTrigger() { }

        public EvenDaysTrigger(TimeSpan startTime, TimeSpan duration)
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
    }
}
