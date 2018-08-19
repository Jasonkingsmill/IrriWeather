using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class AddDayOfWeekScheduleCommand
    {
        public AddDayOfWeekScheduleCommand(IEnumerable<string> days, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            Days = days;
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
        }

        public IEnumerable<string> Days { get; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public bool IsEnabled { get; private set; }
    }
}
