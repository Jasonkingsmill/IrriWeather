using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Scheduling
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
