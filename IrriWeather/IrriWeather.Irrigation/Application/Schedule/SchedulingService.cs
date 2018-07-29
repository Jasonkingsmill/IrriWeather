using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
using Unosquare.PiGpio;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using IrriWeather.Irrigation.Application.Schedule;

namespace IrriWeather.Irrigation.Application
{
    public class SchedulingService
    {
        private readonly ITriggerRepository triggerRepository;
        private readonly JobFactory jobFactory;
        private readonly IScheduler scheduler;

        public SchedulingService(ITriggerRepository triggerRepository, JobFactory jobFactory)
        {
            this.triggerRepository = triggerRepository;
            this.jobFactory = jobFactory;
            this.scheduler = new StdSchedulerFactory().GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
            scheduler.JobFactory = jobFactory;
        }



        public void InitializeScheduler()
        {

            var triggers = triggerRepository.FindAll();
            foreach (var trigger in triggers)
            {
                if (!trigger.IsEnabled)
                    continue;

                IDictionary<string, object> jobData = new Dictionary<string, object>();
                jobData.Add("duration", trigger.Duration);
                jobData.Add("zoneChannels", trigger.Zones.Select(x => x.Channel));


                IJobDetail job = JobBuilder.Create<ZoneJob>()
                    .WithIdentity($"trigger:{trigger.Id}", "zones")
                    .UsingJobData(new JobDataMap(jobData))
                    .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity($"trigger:{trigger.Id}", "zones")
                    .ForJob(job)
                    .WithCronSchedule(trigger.BuildCronExpression())
                    .EndAt(trigger.EnabledUntil)
                    .Build();

                scheduler.ScheduleJob(jobTrigger).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            scheduler.Start();
        }

    }
}
