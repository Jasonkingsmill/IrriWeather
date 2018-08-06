using System;
using Unosquare.PiGpio;
using Unosquare.PiGpio.NativeMethods;

namespace IrriWeather.IO
{
    public class GpioService
    {
        public GpioService()
        {
            var init = Setup.GpioInitialise();
            if (init < 0)
                throw new Exception("Unable to initialise GPIO");

        }


        public SetPinMode(uint pin, PinMode mode)
        {

        }
    }
}
