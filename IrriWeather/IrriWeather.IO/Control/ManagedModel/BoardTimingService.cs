﻿namespace IrriWeather.IO.Control.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Threading;

    /// <summary>
    /// Provides timing, date and delay functions.
    /// Also provides access to registered timers.
    /// </summary>
    public class BoardTimingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardTimingService"/> class.
        /// </summary>
        internal BoardTimingService()
        {
            Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets the Linux epoch (Jan 1, 1970) in UTC.
        /// </summary>
        public DateTime Epoch { get; }

        /// <summary>
        /// Gets the timestamp tick.
        /// Useful to calculate offsets in Alerts or ISR callbacks
        /// </summary>
        public uint TimestampTick => Utilities.GpioTick();

        /// <summary>
        /// Gets the number of seconds elapsed since Jan 1, 1970.
        /// </summary>
        public double TimestampSeconds => Utilities.TimeTime();

        /// <summary>
        /// Gets a timestamp since Jan 1, 1970 in microceconds.
        /// </summary>
        public long TimestampMicroseconds
        {
            get
            {
                Utilities.GpioTime(TimeType.Absolute, out var seconds, out var microseconds);
                return (seconds * 1000000L) + microseconds;
            }
        }

        /// <summary>
        /// Gets the elapsed time since Jan 1, 1970.
        /// </summary>
        public TimeSpan Timestamp => TimeSpan.FromSeconds(TimestampSeconds);

        /// <summary>
        /// Sleeps for the given amount of microseconds.
        /// Waits of 100 microseconds or less use busy waits.
        /// Returns the real elapsed microseconds.
        /// </summary>
        /// <param name="microsecs">The micro seconds.</param>
        /// <returns>Returns the real elapsed microseconds.</returns>
        public long SleepMicros(long microsecs)
        {
            if (microsecs <= 0)
                return 0L;

            if (microsecs <= uint.MaxValue)
                return Threads.GpioDelay(Convert.ToUInt32(microsecs));

            var componentSeconds = microsecs / 1000000d;
            var componentMicrosecs = microsecs % 1000000d;

            if (componentSeconds <= int.MaxValue && componentMicrosecs <= int.MaxValue)
            {
                BoardException.ValidateResult(
                    Threads.GpioSleep(
                        TimeType.Relative,
                        Convert.ToInt32(componentSeconds),
                        Convert.ToInt32(componentMicrosecs)));

                return microsecs;
            }

            Threads.TimeSleep(componentSeconds);
            return microsecs;
        }

        /// <summary>
        /// Sleeps for the specified milliseconds.
        /// </summary>
        /// <param name="millisecs">The milliseconds to sleep for.</param>
        public void Sleep(double millisecs)
        {
            if (millisecs <= 0d)
                return;

            var microsecs = Convert.ToInt64(millisecs * 1000d);
            SleepMicros(microsecs);
        }

        /// <summary>
        /// Sleeps for the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to sleep for.</param>
        public void Sleep(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds <= 0d)
                return;

            Threads.TimeSleep(timeSpan.TotalSeconds);
        }

        /// <summary>
        /// Shortcut method to start a thread.
        /// It runs the thread automatically
        /// </summary>
        /// <param name="doWork">The do work.</param>
        /// <param name="threadName">Name of the thread.</param>
        /// <returns>
        /// A reference to the thread object.
        /// </returns>
        public Thread StartThread(Action doWork, string threadName)
        {
            var thread = new Thread(() => { doWork?.Invoke(); })
            {
                IsBackground = true
            };

            if (string.IsNullOrWhiteSpace(threadName) == false)
                thread.Name = threadName;

            thread.Start();
            return thread;
        }

        /// <summary>
        /// Shortcut method to start a thread.
        /// It runs the thread automatically
        /// </summary>
        /// <param name="doWork">The do work.</param>
        /// <returns>
        /// A reference to the thread object.
        /// </returns>
        public Thread StartThread(Action doWork)
        {
            return StartThread(doWork, null);
        }

        /// <summary>
        /// Starts a timer that executes a block of code with the given period.
        /// </summary>
        /// <param name="periodMilliseconds">The period in milliseconds.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>A reference to a timer.</returns>
        public Timer StartTimer(int periodMilliseconds, Action callback)
        {
            var timer = new Timer((s) => { callback?.Invoke(); }, this, 0, periodMilliseconds);
            return timer;
        }
    }
}
