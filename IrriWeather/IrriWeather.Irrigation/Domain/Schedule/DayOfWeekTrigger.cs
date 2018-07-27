using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DayOfWeekTrigger : Trigger
    {
        private HashSet<DayOfWeek> days = new HashSet<DayOfWeek>();

        private DayOfWeekTrigger(TimeSpan startTime, TimeSpan duration)
        {
            Type = TriggerType.DayOfWeek;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException("Time must be less than 24 hours", nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));

            StartTime = startTime;
            Duration = duration;
        }

        public DayOfWeekTrigger(DayOfWeek day, TimeSpan startTime, TimeSpan duration) : this(startTime, duration)
        {
            days.Add(day);
        }

        public DayOfWeekTrigger(IEnumerable<DayOfWeek> days, TimeSpan startTime, TimeSpan duration) : this(startTime, duration)
        {
            if (days == null || days.Count() == 0)
                throw new ArgumentNullException(nameof(days));

            foreach (var day in days)
                this.days.Add(day);
        }

        public IEnumerable<DayOfWeek> Days { get => this.days; set => this.days = value.ToHashSet(); }
        public TimeSpan StartTime { get; private set; }
    }
}
