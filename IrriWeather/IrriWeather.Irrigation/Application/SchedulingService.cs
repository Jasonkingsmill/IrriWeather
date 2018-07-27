using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
using Unosquare.PiGpio;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace IrriWeather.Irrigation.Application
{
    public class SchedulingService
    {
        private readonly ITriggerRepository triggerRepository;

        public SchedulingService(ITriggerRepository triggerRepository)
        {
            this.triggerRepository = triggerRepository;
        }

        public async Task InitialiseScheduler(IScheduler scheduler)
        {

            var triggers = triggerRepository.FindAll();
            foreach (var trigger in triggers)
            {
                if (!trigger.IsEnabled)
                    continue;

                IJobDetail job = JobBuilder.Create<ZoneJob>()
                .WithIdentity($"{trigger.Id}", "zones")
                .UsingJobData("duration", trigger.Duration.Seconds)
                        .Build();
                
                var triggerBuilder = TriggerBuilder.Create()
                    .WithIdentity($"{trigger.Id}", "zones")
                    .ForJob(job);

                switch (trigger.Type)
                {
                    case TriggerType.DateTime:
                        triggerBuilder.StartAt(((DateTimeTrigger)trigger).StartTime);

                        break;
                    case TriggerType.DayOfMonth:
                        var t = (DayOfMonthTrigger)trigger;
                        triggerBuilder.WithCronSchedule(CronScheduleBuilder.(t.)).StartAt(((DateTimeTrigger)trigger).StartTime);
                        break;
                    case TriggerType.DayOfWeek:
                        break;
                    case TriggerType.EvenDays:
                        break;
                    case TriggerType.OddDays:
                        break;
                }
            }

            TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

        }
    }
}
