using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public class DateTimeTrigger : Trigger
    {
        private DateTimeTrigger() { }

        private DateTimeTrigger(DateTime startTime, TimeSpan duration)
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
    }
}
