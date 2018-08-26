using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class ScheduleDto
    {
        public ScheduleDto(Guid id, string name, string description, string scheduleType, TimeSpan startTime, DateTime startDate, IEnumerable<int> days, bool isEnabled, 
            TimeSpan duration, DateTime enabledUntil, IEnumerable<Guid> zoneIds)
        {
            Id = id;
            Name = name;
            Description = description;
            ScheduleType = scheduleType;
            StartTime = startTime;
            StartDate = startDate;
            Days = days;
            IsEnabled = isEnabled;
            Duration = duration;
            EnabledUntil = enabledUntil;
            ZoneIds = zoneIds;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ScheduleType { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public DateTime StartDate { get; private set; }
        public IEnumerable<int> Days { get; private set; }

        public bool IsEnabled { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public IEnumerable<Guid> ZoneIds { get; private set; }
    }
}
