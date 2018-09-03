using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class UpdateScheduleCommand
    {
        public UpdateScheduleCommand(Guid scheduleId, string name, string description, string scheduleType, IEnumerable<int> days, DateTime startDate, TimeSpan startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled, IEnumerable<Guid> zoneIds)
        {
            ScheduleId = scheduleId;
            Name = name;
            Description = description;
            ScheduleType = scheduleType;
            Days = days;
            StartDate = startDate;
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
            ZoneIds = zoneIds;
        }

        public Guid ScheduleId { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ScheduleType { get; private set; }
        public IEnumerable<int> Days { get; }
        public DateTime StartDate { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EnabledUntil { get; private set; }
        public bool IsEnabled { get; private set; }
        public IEnumerable<Guid> ZoneIds { get; private set; }
    }
}
