using IrriWeather.IO;
using IrriWeather.IO.Control.NativeEnums;
using IrriWeather.Irrigation.Domain.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Control
{
    public class ChannelControlService : IChannelControlService
    {
        private readonly IGpioService gpioService;

        public ChannelControlService(IGpioService gpioService)
        {
            this.gpioService = gpioService;
        }


        public void Start(int channel)
        {
            gpioService.Write(channel, true);
        }


        public void Stop(int channel)
        {
            gpioService.Write(channel, false);
        }

        public void Register(int channel)
        {
            gpioService.RegisterPinControl(channel, PinDirection.Output);
        }

        public void Unregister(int channel)
        {
            gpioService.UnregisterPinControl(channel);
        }

        public bool IsStarted(int channel)
        {
            return gpioService.Read(channel);
        }
    }
}
