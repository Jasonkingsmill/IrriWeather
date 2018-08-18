using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Control;
using IrriWeather.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IrriWeather.Web.Controllers
{
    [Route("api/[controller]")]
    public class IrrigationController : Controller
    {
        private readonly ZoneService zoneService;

        public IrrigationController(ZoneService zoneService)
        {
            this.zoneService = zoneService;
        }

        [HttpGet("zones")]
        public IEnumerable<ZoneSummaryViewModel> GetAll()
        {
            var zones = zoneService.GetZones();
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
            var zone = zoneService.GetZone(id);
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
            var zone = zoneService.AddZone(cmd);
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
            var zone = zoneService.UpdateZone(cmd);
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
            zoneService.RemoveZone(cmd);
            return Ok();
        }






        [HttpPost("zones/{id:guid}/start")]
        public void StartZone(Guid id)
        {
            zoneService.StartZone(id);

        }


        [HttpPost("zones/{id:guid}/stop")]
        public void StopZone(Guid id)
        {
            zoneService.StopZone(id);

        }

    }
}
