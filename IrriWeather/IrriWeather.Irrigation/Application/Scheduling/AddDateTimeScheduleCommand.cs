using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class AddDateTimeScheduleCommand
    {
        public AddDateTimeScheduleCommand(string name, string description, DateTime startDate, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled, IEnumerable<Guid> zoneIds)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
            ZoneIds = zoneIds;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime StartDate { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public bool IsEnabled { get; private set; }
        public IEnumerable<Guid> ZoneIds { get; }
    }
}
