﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public enum TriggerType
    {
        DaysOfWeek,
        DaysOfMonth,
        EvenDays,
        OddDays,
        DateTime
    }
}
