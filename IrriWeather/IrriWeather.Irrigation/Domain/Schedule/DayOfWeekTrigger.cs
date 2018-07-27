using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DayOfWeekTrigger : Trigger
    {
        public DayOfWeekTrigger(DayOfWeek day, DateTime startTime, TimeSpan duration)
        {
            Type = TriggerType.DayOfWeek;

            Day = day;
            StartTime = startTime;
            Duration = duration;
        }

        public DayOfWeek Day { get; }
        public DateTime StartTime { get; }
        public TimeSpan Duration { get; }
    }
}
