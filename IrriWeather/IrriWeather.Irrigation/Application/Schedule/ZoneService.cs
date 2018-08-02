using IrriWeather.Irrigation.Application.Models;
using IrriWeather.Irrigation.Domain;
using Unosquare.PiGpio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IrriWeather.Irrigation.Domain.Control;

namespace IrriWeather.Irrigation.Application
{
    public class ZoneService : IZoneControlService
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
        

        public void SetState(int zone, bool on)
        {
            Board.Pins[zone].Value = on;
        }

        public bool GetState(int channel)
        {
            return Board.Pins[channel].Value;
        }

        public void InitialiseChannels(List<int> channels)
        {
            foreach (var pin in Board.Pins.Where(x => channels.Contains(x.Value.PinNumber)).Select(x => x.Value))
            {
                pin.Direction = Unosquare.PiGpio.NativeEnums.PinDirection.Output;
                pin.Value = false;
            }
        }
    }
}
