using IrriWeather.Irrigation.Domain.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;
using System.Linq;


namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class ScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IScheduler scheduler;

        public ScheduleService(IScheduleRepository scheduleRepository, IScheduler scheduler)
        {
            this.scheduleRepository = scheduleRepository;
            this.scheduler = scheduler;
        }
        
        public void InitializeScheduler()
        {
            var schedules = scheduleRepository.FindAll();
            foreach (var schedule in schedules)
            {
                if (schedule.IsEnabled)
                    AddToScheduler(schedule);
            }

            scheduler.Start();
        }


        public void AddDateTimeSchedule(AddDateTimeScheduleCommand cmd)
        {
            var schedule = new DateTimeSchedule(cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
        }

        public void AddDayOfMonthSchedule(AddDayOfMonthScheduleCommand cmd)
        {
            var schedule = new DayOfMonthSchedule(cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
        }


        public void AddDayOfWeekSchedule(AddDayOfWeekScheduleCommand cmd)
        {

            var schedule = new DayOfWeekSchedule(cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
        }


        public void AddEvenDaysSchedule(AddEvenDaysScheduleCommand cmd)
        {
            var schedule = new EvenDaysSchedule(cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
        }


        public void AddOddDaysSchedule(AddOddDaysScheduleCommand cmd)
        {
            var schedule = new OddDaysSchedule(cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
        }


        public void Remove(RemoveScheduleCommand cmd)
        {
            var schedule = scheduleRepository.Find(cmd.Id);
            if (schedule == null)
                throw new ArgumentException($"Schedule with Id '{cmd.Id}' does not exist");

            scheduleRepository.Remove(schedule);

            var jobKey = BuildJobKey(schedule.Id);
            var jobTrigger = BuildTriggerKey(schedule.Id);

            scheduler.DeleteJob(jobKey);
            scheduler.UnscheduleJob(jobTrigger);
        }

        




        private TriggerKey BuildTriggerKey(Guid scheduleId)
        {
            return new TriggerKey($"schedule:{scheduleId.ToString()}", "zones");
        }


        private JobKey BuildJobKey(Guid scheduleId)
        {
            return new JobKey($"schedule:{scheduleId.ToString()}", "zones");
        }


        private void AddToScheduler(Schedule schedule)
        {
            IDictionary<string, object> jobData = new Dictionary<string, object>();
            jobData.Add("duration", schedule.Duration);
            jobData.Add("zoneChannels", schedule.Zones.Select(x => x.Channel));


            IJobDetail jobDetail = JobBuilder.Create<ZoneJob>()
                .WithIdentity(BuildJobKey(schedule.Id))
                .UsingJobData(new JobDataMap(jobData))
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(BuildTriggerKey(schedule.Id))
                .ForJob(jobDetail)
                .WithCronSchedule(schedule.BuildCronExpression())
                .EndAt(schedule.EnabledUntil)
                .Build();

            scheduler.ScheduleJob(jobDetail, jobTrigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
