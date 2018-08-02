using IrriWeather.Irrigation.Domain.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unosquare.PiGpio;
using Unosquare.PiGpio.NativeEnums;

namespace IrriWeather.Irrigation.Infrastructure.Control
{
    public class ZoneControlService : IZoneControlService
    {

        public void SetState(int zone, bool on)
        {
            Board.Pins[zone].Value = on;
        }

        public bool GetState(int zone)
        {
            return Board.Pins[zone].Value;
        }

    }
}
