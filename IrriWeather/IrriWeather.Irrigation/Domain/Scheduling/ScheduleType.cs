using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public enum ScheduleType
    {
        DaysOfWeek,
        DaysOfMonth,
        EvenDays,
        OddDays,
        DateTime
    }
}
