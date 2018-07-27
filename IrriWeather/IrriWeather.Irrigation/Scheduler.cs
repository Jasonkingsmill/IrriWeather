using System;
using System.Collections.Generic;
using System.Text;
using Quartz;

namespace IrriWeather.Irrigation
{
    public class Scheduler
    {
        public Scheduler()
        {
        }


        void start()
        {
            CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek();

            var trigger = TriggerBuilder.Create()
            .WithIdentity("trigger3", "group1")
            
            .WithCronSchedule("0 0/2 8-17 * * ?")
            .ForJob("myJob", "group1")
            .Build();

        }
    }
}
