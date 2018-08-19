using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class AddDayOfMonthScheduleCommand
    {
        public AddDayOfMonthScheduleCommand(IEnumerable<int> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            Days = days;
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
        }

        public IEnumerable<int> Days { get; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public bool IsEnabled { get; private set; }
    }
}
