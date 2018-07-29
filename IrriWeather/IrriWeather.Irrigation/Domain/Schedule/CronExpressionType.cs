using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public enum CronExpressionType
    {
        AtSpecificDateTime,
        EveryNMinutes,
        EveryNHours,
        EveryDayAt,
        EveryNDaysAt,
        EveryWeekDay,
        EverySpecificWeekDayAt,
        EverySpecificDayEveryNMonthAt,
        EverySpecificSeqWeekDayEveryNMonthAt,
        EverySpecificDayOfMonthAt,
        EverySpecificSeqWeekDayOfMonthAt,
        EverySpecificDaysEveryNMonthAt,
    }
}
