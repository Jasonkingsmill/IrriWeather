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


        public IEnumerable<ScheduleDto> GetSchedules()
        {
            var schedules = scheduleRepository.FindAll();
            var models = schedules.Select(sched => new ScheduleDto(sched.Id, sched.Name, sched.Description, sched.ScheduleType.ToString(), sched.StartTime, 
                sched.StartDate, sched.Days, sched.IsEnabled, sched.Duration, sched.EnabledUntil, sched.Zones.Select(y=>y.Id)));
            return models;
        }

        
        public ScheduleDto AddDateTimeSchedule(AddDateTimeScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDateTimeSchedule(cmd.Name, cmd.Description, cmd.StartDate, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled, 
                schedule.Duration, schedule.EnabledUntil, schedule.Zones.Select(x=>x.Id));
        }

        public ScheduleDto AddDayOfMonthSchedule(AddDayOfMonthScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDayOfMonthSchedule(cmd.Name, cmd.Description, cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.Zones.Select(x => x.Id));
        }


        public ScheduleDto AddDayOfWeekSchedule(AddDayOfWeekScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDayOfWeekSchedule(cmd.Name, cmd.Description, cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.Zones.Select(x => x.Id));
        }


        public ScheduleDto AddEvenDaysSchedule(AddEvenDaysScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateEvenDaysSchedule(cmd.Name, cmd.Description, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.Zones.Select(x => x.Id));
        }


        public ScheduleDto AddOddDaysSchedule(AddOddDaysScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateOddDaysSchedule(cmd.Name, cmd.Description, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.Zones.Select(x => x.Id));
        }


        public void RemoveSchedule(RemoveScheduleCommand cmd)
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
