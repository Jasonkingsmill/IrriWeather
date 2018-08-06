using IrriWeather.Irrigation.Domain;
using Unosquare.PiGpio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IrriWeather.Irrigation.Domain.Control;

namespace IrriWeather.Irrigation.Application.Control
{
    public class ZoneService
    {
        private readonly IZoneRepository zoneRepository;
        private readonly IChannelControlService controlService;

        public ZoneService(IZoneRepository zoneRepository, IChannelControlService controlService)
        {
            this.zoneRepository = zoneRepository;
            this.controlService = controlService;
        }

        public IEnumerable<ZoneDto> GetZones()
        {
            var zones = zoneRepository.FindAll();
            return zones.Select(zone => new ZoneDto(zone.Id, zone.Name, zone.Description, zone.Channel, zone.IsEnabled));
        }

        public ZoneDto GetZone(Guid id)
        {
            var zone = zoneRepository.Find(id);
            return new ZoneDto(zone.Id, zone.Name, zone.Description, zone.Channel, zone.IsEnabled);
        }


        public void RunZone(Guid zoneId)
        {
            var zone = zoneRepository.Find(zoneId);
            if (zone == null)
                throw new ArgumentNullException(nameof(zoneId), $"The zone '{zoneId}' does not exist");

            zone.Run(controlService);
        }

        public ZoneDto AddZone(AddZoneCommand cmd)
        {
            var zone = new Zone(cmd.Name, cmd.Description, cmd.Channel, cmd.IsEnabled);
            zoneRepository.Add(zone);
            return new ZoneDto(zone.Id, zone.Name, zone.Description, zone.Channel, zone.IsEnabled);
        }

        public void RemoveZone(RemoveZoneCommand cmd)
        {
            var zone = zoneRepository.Find(cmd.Id);
            if (zone == null)
                throw new ArgumentException($"A zone with id '{cmd.Id}' does not exist");
            zoneRepository.Remove(zone);
        }

        public void StopZone(Guid zoneId)
        {
            var zone = zoneRepository.Find(zoneId);
            if (zone == null)
                throw new ArgumentNullException(nameof(zoneId), $"The zone '{zoneId}' does not exist");

            zone.Stop(controlService);
        }

    }
}
