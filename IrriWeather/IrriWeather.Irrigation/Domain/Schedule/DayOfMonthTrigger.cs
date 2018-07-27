﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DayOfMonthTrigger : Trigger
    {
        private HashSet<int> days = new HashSet<int>();

        private DayOfMonthTrigger() { }

        private DayOfMonthTrigger(TimeSpan startTime, TimeSpan duration)
        {
            Type = TriggerType.DayOfMonth;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException("Time must be less than 24 hours", nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));

            StartTime = startTime;
            Duration = duration;
        }

        public DayOfMonthTrigger(int day, TimeSpan startTime, TimeSpan duration) : this(startTime, duration)
        {
            if (day > 31 || day < 1)
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
            days.Add(day);
        }

        public DayOfMonthTrigger(IEnumerable<int> days, TimeSpan startTime, TimeSpan duration) : this(startTime, duration)
        {
            foreach (var day in days)
            {
                if (day > 31 || day < 1)
                    throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
                this.days.Add(day);
            }
        }

        public IEnumerable<int> Days { get => this.days;  set => this.days = value.ToHashSet(); }
        public TimeSpan StartTime { get; private set; }
    }
}
