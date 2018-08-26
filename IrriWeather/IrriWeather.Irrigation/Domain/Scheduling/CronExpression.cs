using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public class CronExpression
    {
        private readonly DaysOfWeek _days;
        private readonly Months _month;
        private readonly int _interval;
        private readonly int _dayNumber;
        private readonly List<int> _dayNumbers;
        private readonly int _startHour;
        private readonly int _startMinute;
        private readonly int _year;

        private readonly CronExpressionType _expressionType;
        private string _cronString;

        public CronExpressionType ExpressionType
        {
            get { return _expressionType; }
        }

        private void BuildCronExpression()
        {
            switch (ExpressionType)
            {
                case CronExpressionType.AtSpecificDateTime:
                    _cronString = string.Format("0 {0} {1} {2} {3} ? {4}", _startMinute, _startHour, _dayNumber, (int)_month, _year);
                    break;
                case CronExpressionType.EveryNMinutes:
                    _cronString = string.Format("0 0/{0} * 1/1 * ? *", _interval);
                    break;
                case CronExpressionType.EveryNHours:
                    _cronString = string.Format("0 0 0/{0} 1/1 * ? *", _interval);
                    break;
                case CronExpressionType.EveryNDaysAt:
                case CronExpressionType.EveryDayAt:
                    _cronString = string.Format("0 {0} {1} 1/{2} * ? *", _startMinute, _startHour, _interval);
                    break;
                case CronExpressionType.EveryWeekDay:
                    _cronString = string.Format("0 {0} {1} ? * MON-FRI *", _startMinute, _startHour);
                    break;
                case CronExpressionType.EverySpecificWeekDayAt:
                    if (_dayNumbers != null && _dayNumbers.Count != 0)
                        _cronString = string.Format("0 {0} {1} ? * {2} *", _startMinute, _startHour, CronConverter.ToCronWeekdayRepresentation(_dayNumbers));
                    else
                        _cronString = string.Format("0 {0} {1} ? * {2} *", _startMinute, _startHour, CronConverter.ToCronRepresentation(_days));
                    break;
                case CronExpressionType.EverySpecificDayEveryNMonthAt:
                    _cronString = string.Format("0 {0} {1} {2} {3} ? *", _startMinute, _startHour, _dayNumber, _interval);
                    break;
                case CronExpressionType.EverySpecificDaysEveryNMonthAt:
                    _cronString = string.Format("0 {0} {1} {2} 1/{3} ? *", _startMinute, _startHour, CronConverter.ToCronRepresentation(_dayNumbers), _interval);
                    break;
                case CronExpressionType.EverySpecificSeqWeekDayEveryNMonthAt:
                    _cronString = string.Format("0 {0} {1} ? 1/{2} {3}#{4} *", _startMinute, _startHour, _interval, CronConverter.ToCronRepresentationSingle(_days), _dayNumber);
                    break;
                case CronExpressionType.EverySpecificDayOfMonthAt:
                    _cronString = string.Format("0 {0} {1} {2} {3} ? *", _startMinute, _startHour, _dayNumber, (int)_month);
                    break;
                case CronExpressionType.EverySpecificSeqWeekDayOfMonthAt:
                    _cronString = string.Format("0 {0} {1} ? {2} {3}#{4} *", _startMinute, _startHour, (int)_month, CronConverter.ToCronRepresentationSingle(_days), _dayNumber);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        protected CronExpression(int interval, CronExpressionType expressionType)
        {
            _interval = interval;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(int startHour, int startMinute, CronExpressionType expressionType)
        {
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(int interval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _interval = interval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(DaysOfWeek days, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _days = days;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(IEnumerable<string> days, int startHour, int startMinute, CronExpressionType expressionType)
        {
            var tempDays = days.Select(x => Enum.Parse<DaysOfWeek>(x));
            foreach (var day in tempDays)
            {
                _days = day | _days;
            }
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(int dayNumber, int interval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = dayNumber;
            _interval = interval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(DaySeqNumber dayNumber, DaysOfWeek days, int monthInverval, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = (int)dayNumber;
            _days = days;
            _interval = monthInverval;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(Months month, int dayNumber, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _month = month;
            _dayNumber = dayNumber;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(DaySeqNumber dayNumber, DaysOfWeek days, Months month, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = (int)dayNumber;
            _days = days;
            _month = month;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;

            BuildCronExpression();
        }

        protected CronExpression(int dayNumber, Months month, int year, int startHour, int startMinute, CronExpressionType expressionType)
        {
            _dayNumber = dayNumber;
            _month = month;
            _startHour = startHour;
            _startMinute = startMinute;
            _expressionType = expressionType;
            _year = year;

            BuildCronExpression();
        }



        public CronExpression(IEnumerable<int> dayNumbers, int hour, int minute, CronExpressionType expressionType)
        {
            _dayNumbers = dayNumbers.ToList();
            _startHour = hour;
            _startMinute = minute;
            _expressionType = expressionType;

            BuildCronExpression();

        }



        /// <summary>
        /// Create new CronExpression instance, which occurs every *minutesInteval* minutes
        /// </summary>
        /// <param name="minutesInteval">Interval in minutes</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EveryNMinutes(int minutesInteval)
        {
            var ce = new CronExpression(minutesInteval, CronExpressionType.EveryNMinutes);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every *hoursInterval* hours
        /// </summary>
        /// <param name="hoursInterval">Interval in hours</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EveryNHours(int hoursInterval)
        {
            var ce = new CronExpression(hoursInterval, CronExpressionType.EveryNHours);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every day at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EveryDayAt(int hour, int minute)
        {
            var ce = new CronExpression(1, hour, minute, CronExpressionType.EveryDayAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every *daysInterval* days at specified hours
        /// </summary>
        /// <param name="daysInterval">Interval in days</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EveryNDaysAt(int daysInterval, int hour, int minute)
        {
            var ce = new CronExpression(daysInterval, hour, minute, CronExpressionType.EveryNDaysAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs monday to friday at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EveryWeekDayAt(int hour, int minute)
        {
            var ce = new CronExpression(hour, minute, CronExpressionType.EveryWeekDay);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on specified days at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <param name="days">Days, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificWeekDayAt(int hour, int minute, DaysOfWeek days)
        {
            var ce = new CronExpression(days, hour, minute, CronExpressionType.EverySpecificWeekDayAt);
            return ce;
        }



        /// <summary>
        /// Create new CronExpression instance, which occurs on specified week days at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <param name="days">Days, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificWeekDayAt(int hour, int minute, IEnumerable<int> dayNumbers)
        {
            var ce = new CronExpression(dayNumbers, hour, minute, CronExpressionType.EverySpecificWeekDayAt);
            return ce;
        }



        /// <summary>
        /// Create new CronExpression instance, which occurs on specified days at specified hours
        /// </summary>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <param name="days">Days, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificWeekDayAt(int hour, int minute, IEnumerable<string> days)
        {
            var ce = new CronExpression(days, hour, minute, CronExpressionType.EverySpecificWeekDayAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every specific day of month 
        /// every *monthInterval* month at specified hours
        /// </summary>
        /// <param name="dayNumber">Day of the month, when occurence will happen</param>
        /// <param name="monthInterval">Interval in months</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificDayEveryNMonthAt(int dayNumber, int monthInterval, int hour, int minute)
        {
            var ce = new CronExpression(dayNumber, monthInterval, hour, minute, CronExpressionType.EverySpecificDayEveryNMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs every specific day of month 
        /// every *monthInterval* month at specified hours
        /// </summary>
        /// <param name="dayNumber">Day of the month, when occurence will happen</param>
        /// <param name="monthInterval">Interval in months</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificDaysEveryNMonthAt(IEnumerable<int> dayNumbers, int monthInterval, int hour, int minute)
        {
            var ce = new CronExpression(dayNumbers, hour, minute, CronExpressionType.EverySpecificDaysEveryNMonthAt);
            return ce;
        }

        // TODO Add comments
        /// <summary>
        /// Create new CronExpression instance, which occurs on every first, 
        /// second, third or fourth day of the week each *monthInterval* months
        /// at specified hours
        /// </summary>
        /// <param name="dayNumber">Day sequental number (first, second, third, fourth)</param>
        /// <param name="days">Day of week</param>
        /// <param name="monthInverval">Interval in months</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificSeqWeekDayEveryNMonthAt(DaySeqNumber dayNumber, DaysOfWeek days, int monthInverval, int hour, int minute)
        {
            var ce = new CronExpression(dayNumber, days, monthInverval, hour, minute, CronExpressionType.EverySpecificSeqWeekDayEveryNMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on every specific day *dayNumber* of 
        /// specific month *month* at specified hours
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="dayNumber">Day number</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificDayOfMonthAt(Months month, int dayNumber, int hour, int minute)
        {
            var ce = new CronExpression(month, dayNumber, hour, minute, CronExpressionType.EverySpecificDayOfMonthAt);
            return ce;
        }

        /// <summary>
        /// Create new CronExpression instance, which occurs on every first, 
        /// second, third or fourth day of the week on specific month *month*
        /// at specified hours
        /// </summary>
        /// <param name="dayNumber">Day sequental number (first, second, third, fourth)</param>
        /// <param name="days">Day of week</param>
        /// <param name="month">Month</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression EverySpecificSeqWeekDayOfMonthAt(DaySeqNumber dayNumber, DaysOfWeek days, Months month, int hour, int minute)
        {
            var ce = new CronExpression(dayNumber, days, month, hour, minute, CronExpressionType.EverySpecificSeqWeekDayOfMonthAt);
            return ce;
        }


        /// <summary>
        /// Create new CronExpression instance, which occurs on every specific day *dayNumber* of 
        /// specific month *month* at specified hours
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="dayNumber">Day number</param>
        /// <param name="hour">Hour, when occurence will happen</param>
        /// <param name="minute">Minute, when occurence will happen</param>
        /// <returns>New CronExpression instance</returns>
        public static CronExpression AtSpecificDateTime(int year, Months month, int dayNumber, int hour, int minute)
        {
            var ce = new CronExpression(dayNumber, month, year, hour, minute, CronExpressionType.AtSpecificDateTime);
            return ce;
        }

        public override string ToString()
        {
            return _cronString;
        }

        public static implicit operator string(CronExpression expression)
        {
            return expression.ToString();
        }
    }
}
