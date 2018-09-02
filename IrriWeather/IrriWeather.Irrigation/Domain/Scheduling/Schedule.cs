using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Common.Domain;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class Schedule : Entity
    {
        //private string daysAsString { get; set; }

        //private HashSet<int> _days { get => daysAsString.Split(";").Select(x => int.Parse(x)).ToHashSet(); set => this.daysAsString = string.Join(";", value); }


        private HashSet<Guid> _zoneIds = new HashSet<Guid>();
        private HashSet<int> _days = new HashSet<int>();

        private Schedule()
        {
        }

        private Schedule(string name, string description, TimeSpan startTime, TimeSpan duration, DateTime? enabledUntil, bool isEnabled)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name), "Name must not be empty");
            if (startTime == null)
                throw new ArgumentNullException(nameof(duration));
            if (startTime >= TimeSpan.FromDays(1))
                throw new ArgumentException(nameof(duration), "StarTime cannot be greater than 24:00");

            if (duration == null)
                throw new ArgumentNullException(nameof(duration));
            if (duration >= TimeSpan.FromDays(1))
                throw new ArgumentException(nameof(duration), "Duration cannot be greater than 24 hours");

            if (enabledUntil != null && enabledUntil < DateTime.Now)
                throw new ArgumentException("EnabledUntil must be greater than now", nameof(enabledUntil));

            Name = name;
            Description = description ?? "";
            StartTime = startTime;
            Duration = duration;
            EnabledUntil = enabledUntil ?? DateTime.MaxValue;
            IsEnabled = isEnabled;
        }



        internal Schedule(ScheduleType scheduleType, string name, string description, IEnumerable<int> days, DateTime? startDate, TimeSpan startTime, TimeSpan duration, DateTime? enabledUntil, bool isEnabled) : this(name, description, startTime, duration, enabledUntil, isEnabled)
        {
            ScheduleType = scheduleType;

            switch (scheduleType)
            {
                case ScheduleType.DateTime:
                    if (startDate == null)
                        throw new ArgumentNullException(nameof(startDate));
                    if (((DateTime)startDate).Date < DateTime.Now.Date)
                        throw new ArgumentException(nameof(startDate), "StartDate must be into the future");
                    StartDate = (DateTime)startDate;
                    break;

                case ScheduleType.DaysOfMonth:
                case ScheduleType.DaysOfWeek:
                    foreach (var day in days)
                        AddDay(day);
                    break;

                case ScheduleType.EvenDays:
                case ScheduleType.OddDays:
                default:
                    throw new Exception();
            }


        }




        public string Name { get; private set; }
        public string Description { get; private set; }
        public ScheduleType ScheduleType { get; protected set; }
        public TimeSpan StartTime { get; private set; }
        public DateTime StartDate { get; private set; }
        public IEnumerable<int> Days { get { return _days.ToHashSet(); } private set { _days = value.ToHashSet(); } }


        public bool IsEnabled { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public DateTime EnabledUntil { get; protected set; }
        public IEnumerable<Guid> ZoneIds { get => _zoneIds.ToHashSet(); private set => _zoneIds = value.ToHashSet(); }



        public void AddDay(int day)
        {
            switch (ScheduleType)
            {
                case ScheduleType.DaysOfMonth:
                    if (day > 31 || day < 1)
                        throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
                    this._days.Add(day);
                    break;
                case ScheduleType.DaysOfWeek:
                    if (day > 7 || day < 1)
                        throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 7");
                    this._days.Add(day);
                    break;
                default:
                    throw new Exception($"Schedule type '{ScheduleType.ToString()}' does not support days");
            }
        }



        public void SetEnablement(bool enabled)
        {
            IsEnabled = enabled;
        }


        public void UpdateZones(IEnumerable<Guid> zoneIds)
        {
            this._zoneIds.Clear();
            this.ZoneIds = zoneIds.ToHashSet();
        }

        public void AttachZone(Guid zoneId)
        {
            this._zoneIds.Add(zoneId);
        }

        public void DetachZone(Guid zoneId)
        {
            this._zoneIds.Remove(zoneId);
        }

        public string BuildCronExpression()
        {
            switch (ScheduleType)
            {
                case ScheduleType.DateTime:
                    return CronExpression.AtSpecificDateTime(StartDate.Year, (Months)StartDate.Month, StartDate.Day, StartTime.Hours, StartTime.Minutes);
                case ScheduleType.DaysOfMonth:
                    return CronExpression.EverySpecificDaysEveryNMonthAt(Days, 1, StartTime.Hours, StartTime.Minutes);
                case ScheduleType.DaysOfWeek:
                    return CronExpression.EverySpecificWeekDayAt(StartTime.Hours, StartTime.Minutes, Days);
                case ScheduleType.EvenDays:
                    return CronExpression.EverySpecificDaysEveryNMonthAt(new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 }, 1, StartTime.Hours, StartTime.Minutes);
                case ScheduleType.OddDays:
                    return CronExpression.EverySpecificDaysEveryNMonthAt(new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31 }, 1, StartTime.Hours, StartTime.Minutes);
            }
            return null;
        }
    }
}
