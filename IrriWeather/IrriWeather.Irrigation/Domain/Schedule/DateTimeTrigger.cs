using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DateTimeTrigger : Trigger
    {
        private DateTimeTrigger() { }

        public DateTimeTrigger(DateTime startTime, TimeSpan duration, DateTime enabledUntil, bool isEnabled) : base(duration, enabledUntil, isEnabled)
        {
            Type = TriggerType.DateTime;

            if (startTime == null)
                throw new ArgumentNullException(nameof(startTime));
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));
            StartTime = startTime;
            Duration = duration;
        }
        

        public DateTime StartTime { get; private set; }

        public override string BuildCronExpression()
        {
            return CronExpression.AtSpecificDateTime(StartTime.Year, (Months)StartTime.Month, StartTime.Day, StartTime.Hour, StartTime.Minute);
        }
    }
}
