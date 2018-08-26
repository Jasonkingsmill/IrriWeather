using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class AddEvenDaysScheduleCommand
    {
        public AddEvenDaysScheduleCommand(string name, string description, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            Name = name;
            Description = description;
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
        }

        public string Name { get; }
        public string Description { get; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public bool IsEnabled { get; private set; }
    }
}
