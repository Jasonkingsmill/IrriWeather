using IrriWeather.Irrigation.Domain.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;
using System.Linq;
using IrriWeather.Irrigation.Domain.Control;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class ScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IZoneRepository zoneRepository;
        private readonly IScheduler scheduler;

        public ScheduleService(IScheduleRepository scheduleRepository, IZoneRepository zoneRepository, IScheduler scheduler)
        {
            this.scheduleRepository = scheduleRepository;
            this.zoneRepository = zoneRepository;
            this.scheduler = scheduler;
        }
        
        public void InitializeScheduler()
        {
            var schedules = scheduleRepository.FindAll();
            foreach (var schedule in schedules)
            {
                if (schedule.IsEnabled && schedule.EnabledUntil > DateTime.Now)
                    AddToScheduler(schedule);
            }

            scheduler.Start();
        }


        public IEnumerable<ScheduleDto> GetSchedules()
        {
            var schedules = scheduleRepository.FindAll();
            var models = schedules.Select(sched => new ScheduleDto(sched.Id, sched.Name, sched.Description, sched.ScheduleType.ToString(), sched.StartTime, 
                sched.StartDate, sched.Days, sched.IsEnabled, sched.Duration, sched.EnabledUntil, sched.ZoneIds)).ToList();
            return models;
        }

        
        public ScheduleDto AddDateTimeSchedule(AddDateTimeScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDateTimeSchedule(cmd.Name, cmd.Description, cmd.StartDate, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            foreach(var id in cmd.ZoneIds)
            {
                var zone = zoneRepository.Find(id);
                if (zone == null)
                    throw new Exception($"Zone with id '{id}' does not exist");
                schedule.AttachZone(id);
            }
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled, 
                schedule.Duration, schedule.EnabledUntil, schedule.ZoneIds);
        }

        public ScheduleDto AddDayOfMonthSchedule(AddDayOfMonthScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDayOfMonthSchedule(cmd.Name, cmd.Description, cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            foreach (var id in cmd.ZoneIds)
            {
                var zone = zoneRepository.Find(id);
                if (zone == null)
                    throw new Exception($"Zone with id '{id}' does not exist");
                schedule.AttachZone(id);
            }
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.ZoneIds);
        }


        public ScheduleDto AddDayOfWeekSchedule(AddDayOfWeekScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateDayOfWeekSchedule(cmd.Name, cmd.Description, cmd.Days, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            foreach (var id in cmd.ZoneIds)
            {
                var zone = zoneRepository.Find(id);
                if (zone == null)
                    throw new Exception($"Zone with id '{id}' does not exist");
                schedule.AttachZone(id);
            }
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.ZoneIds);
        }


        public ScheduleDto AddEvenDaysSchedule(AddEvenDaysScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateEvenDaysSchedule(cmd.Name, cmd.Description, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            foreach (var id in cmd.ZoneIds)
            {
                var zone = zoneRepository.Find(id);
                if (zone == null)
                    throw new Exception($"Zone with id '{id}' does not exist");
                schedule.AttachZone(id);
            }
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.ZoneIds);
        }


        public ScheduleDto AddOddDaysSchedule(AddOddDaysScheduleCommand cmd)
        {
            var factory = new ScheduleFactory();
            Schedule schedule = factory.CreateOddDaysSchedule(cmd.Name, cmd.Description, cmd.StartTime, cmd.Duration, cmd.EnabledUntil, cmd.IsEnabled);
            scheduleRepository.Add(schedule);

            AddToScheduler(schedule);
            return new ScheduleDto(schedule.Id, schedule.Name, schedule.Description, schedule.ScheduleType.ToString(), schedule.StartTime, schedule.StartDate, schedule.Days, schedule.IsEnabled,
                schedule.Duration, schedule.EnabledUntil, schedule.ZoneIds);
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
            jobData.Add("scheduleId", schedule.Id);


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
