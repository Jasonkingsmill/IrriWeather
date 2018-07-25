using IrriWeather.Irrigation.Application.Models;
using IrriWeather.Irrigation.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Application
{
    public class ZoneService
    {
        private readonly IZoneRepository zoneRepository;

        public ZoneService(IZoneRepository zoneRepository)
        {
            this.zoneRepository = zoneRepository;
        }

        public IEnumerable<ZoneModel> GetZones()
        {
            var zones = zoneRepository.FindAll();
            return zones.Select(x => new ZoneModel(x.Name, x.Description, x.Channel, x.IsEnabled));            
        }
    }
}
