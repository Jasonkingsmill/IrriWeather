using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Control;
using IrriWeather.Irrigation.Application.Scheduling;
using IrriWeather.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IrriWeather.Web.Controllers
{
    [Route("api/[controller]")]
    public class IrrigationController : Controller
    {
        private readonly ZoneService _zoneService;
        private readonly ScheduleService _scheduleService;

        public IrrigationController(ZoneService zoneService, ScheduleService scheduleService)
        {
            this._zoneService = zoneService;
            this._scheduleService = scheduleService;
        }




        #region Zones



        [HttpGet("zones")]
        public IEnumerable<ZoneSummaryViewModel> GetAllZones()
        {
            var zones = _zoneService.GetZones();
            return zones.Select(zone => new ZoneSummaryViewModel()
            {
                Id = zone.Id,
                Channel = zone.Channel,
                Description = zone.Description,
                IsEnabled = zone.IsEnabled,
                Name = zone.Name,
                IsStarted = zone.IsStarted

            });
        }


        [HttpGet("zones/{id:guid}")]
        public ZoneSummaryViewModel GetZone(Guid id)
        {
            var zone = _zoneService.GetZone(id);
            return new ZoneSummaryViewModel()
            {
                Id = zone.Id,
                Channel = zone.Channel,
                Description = zone.Description,
                IsEnabled = zone.IsEnabled,
                Name = zone.Name,
                IsStarted = zone.IsStarted
            };
        }


        [HttpPost("zones")]
        public IActionResult AddZone([FromBody]AddZoneViewModel model)
        {
            var cmd = new AddZoneCommand(model.Name, model.Description, model.Channel, model.IsEnabled);
            var zone = _zoneService.AddZone(cmd);
            var newZone = new ZoneSummaryViewModel()
            {
                Id = zone.Id,
                Channel = zone.Channel,
                Description = zone.Description,
                IsEnabled = zone.IsEnabled,
                Name = zone.Name
            };
            return Created(newZone.Id.ToString(), newZone);
        }


        [HttpPut("zones/{id:guid}")]
        public IActionResult UpdateZone(Guid id, [FromBody]UpdateZoneViewModel model)
        {
            var cmd = new UpdateZoneCommand(id, model.Name, model.Description, model.Channel, model.IsEnabled);
            var zone = _zoneService.UpdateZone(cmd);
            var newZone = new ZoneSummaryViewModel()
            {
                Id = zone.Id,
                Channel = zone.Channel,
                Description = zone.Description,
                IsEnabled = zone.IsEnabled,
                Name = zone.Name
            };
            return Created(newZone.Id.ToString(), newZone);
        }


        [HttpDelete("zones/{id:guid}")]
        public IActionResult RemoveZone(Guid id)
        {
            var cmd = new RemoveZoneCommand(id);
            _zoneService.RemoveZone(cmd);
            return Ok();
        }



        [HttpPost("zones/{id:guid}/start")]
        public void StartZone(Guid id)
        {
            _zoneService.StartZone(id);

        }


        [HttpPost("zones/{id:guid}/stop")]
        public void StopZone(Guid id)
        {
            _zoneService.StopZone(id);

        }


        #endregion





        #region Schedules


        [HttpGet("schedules")]
        public IEnumerable<ScheduleSummaryViewModel> GetAllSchedules()
        {
            var schedules = _scheduleService.GetSchedules();
            return schedules.Select(sched => new ScheduleSummaryViewModel()
            {
                Id = sched.Id,
                Name = sched.Name,
                Description = sched.Description,
                ScheduleType = sched.ScheduleType,
                Days = sched.Days,
                Duration = sched.Duration,
                EnabledUntil = sched.EnabledUntil,
                IsEnabled = sched.IsEnabled,
                StartDate = sched.StartDate,
                StartTime = sched.StartTime,
                ZoneIds = sched.ZoneIds
            });
        }




        [HttpPost("schedules")]
        public IActionResult AddSchedule([FromBody]AddScheduleViewModel model)
        {
            var cmd = new AddScheduleCommand(model.Name, model.Description, model.ScheduleType, model.Days, model.StartDate, model.StartTime, model.Duration, model.EnabledUntil, model.IsEnabled, model.ZoneIds);
            var sched = _scheduleService.AddSchedule(cmd);
            var newschedule = new ScheduleSummaryViewModel()
            {
                Id = sched.Id,
                Name = sched.Name,
                Description = sched.Description,
                ScheduleType = sched.ScheduleType,
                Days = sched.Days,
                Duration = sched.Duration,
                EnabledUntil = sched.EnabledUntil,
                IsEnabled = sched.IsEnabled,
                StartDate = sched.StartDate,
                StartTime = sched.StartTime,
                ZoneIds = sched.ZoneIds
            };
            return Created(newschedule.Id.ToString(), newschedule);
        }



        [HttpPut("schedules/{scheduleId:guid}")]
        public IActionResult UpdateSchedule(Guid scheduleId, [FromBody]AddScheduleViewModel model)
        {
            var cmd = new UpdateScheduleCommand(scheduleId, model.Name, model.Description, model.ScheduleType, model.Days, model.StartDate, model.StartTime, model.Duration, model.EnabledUntil, model.IsEnabled, model.ZoneIds);
            var sched = _scheduleService.UpdateSchedule(cmd);
            var newschedule = new ScheduleSummaryViewModel()
            {
                Id = sched.Id,
                Name = sched.Name,
                Description = sched.Description,
                ScheduleType = sched.ScheduleType,
                Days = sched.Days,
                Duration = sched.Duration,
                EnabledUntil = sched.EnabledUntil,
                IsEnabled = sched.IsEnabled,
                StartDate = sched.StartDate,
                StartTime = sched.StartTime,
                ZoneIds = sched.ZoneIds
            };
            return Created(newschedule.Id.ToString(), newschedule);
        }



        [HttpDelete("schedules/{id:guid}")]
        public IActionResult RemoveSchedule(Guid id)
        {
            var cmd = new RemoveScheduleCommand(id);
            _scheduleService.RemoveSchedule(cmd);
            return Ok();

        }
        #endregion
    }
}
