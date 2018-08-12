using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace IrriWeather.Irrigation.Application.Schedule
{
    public class SchedulingService
    {
        private readonly ITriggerRepository triggerRepository;
        private readonly JobFactory jobFactory;
        private readonly IScheduler scheduler;

        public SchedulingService(ITriggerRepository triggerRepository, IScheduler scheduler)
        {
            this.triggerRepository = triggerRepository;
            this.scheduler = scheduler;
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


                IJobDetail jobDetail = JobBuilder.Create<ZoneJob>()
                    .WithIdentity($"trigger:{trigger.Id}", "zones")
                    .UsingJobData(new JobDataMap(jobData))
                    .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity($"trigger:{trigger.Id}", "zones")
                    .ForJob(jobDetail)
                    .WithCronSchedule(trigger.BuildCronExpression())
                    .EndAt(trigger.EnabledUntil)
                    .Build();

                scheduler.ScheduleJob(jobDetail, jobTrigger).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            scheduler.Start();
        }


        public void Add(Trigger trigger)
        {
            IDictionary<string, object> jobData = new Dictionary<string, object>();
            jobData.Add("duration", trigger.Duration);
            jobData.Add("zoneChannels", trigger.Zones.Select(x => x.Channel));


            IJobDetail jobDetail = JobBuilder.Create<ZoneJob>()
                .WithIdentity(BuildJobKey(trigger.Id))
                .UsingJobData(new JobDataMap(jobData))
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(BuildTriggerKey(trigger.Id))
                .ForJob(jobDetail)
                .WithCronSchedule(trigger.BuildCronExpression())
                .EndAt(trigger.EnabledUntil)
                .Build();

            scheduler.ScheduleJob(jobDetail, jobTrigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }


        public void Remove(Trigger trigger)
        {
            var jobKey = BuildJobKey(trigger.Id);
            var jobTrigger = BuildTriggerKey(trigger.Id);

            scheduler.DeleteJob(jobKey);
            scheduler.UnscheduleJob(jobTrigger);
        }

        




        private TriggerKey BuildTriggerKey(Guid triggerId)
        {
            return new TriggerKey($"trigger:{triggerId}", "zones");
        }


        private JobKey BuildJobKey(Guid triggerId)
        {
            return new JobKey($"trigger:{triggerId}", "zones");
        }
    }
}
