using IrriWeather.IO;
using IrriWeather.Irrigation.Domain.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Control
{
    public class ChannelControlService : IChannelControlService
    {
        private readonly GpioService gpioService;

        public ChannelControlService(GpioService gpioService)
        {
            this.gpioService = gpioService;
        }

        public void SetState(int zone, bool on)
        {
            gpioService.
            Board.Pins[zone].Value = on;
        }

        public bool GetState(int zone)
        {
            return Board.Pins[zone].Value;
        }

    }
}
