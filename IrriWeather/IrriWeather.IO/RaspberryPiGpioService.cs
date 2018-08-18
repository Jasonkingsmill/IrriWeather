using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrriWeather.IO.Control;
using IrriWeather.IO.Control.NativeEnums;
using Pi = IrriWeather.IO.Control.NativeMethods;
using IrriWeather.IO.Control.NativeTypes;
using IrriWeather.IO.Control.NativeMethods;

namespace IrriWeather.IO
{
    public class RaspberryPiGpioService : IDisposable, IGpioService
    {
        private static object _lock = new object();

        private static bool _initialized = false;


        public RaspberryPiGpioService()
        {
            lock (_lock)
            {
                if (!_initialized)
                {
                    //Retrieve internal configuration
                    var config = (int)Setup.GpioCfgGetInternals();

                    //config = config.ApplyBits(false, 3, 2, 1, 0); // Clear debug flags
                    //             config = config | (int) ConfigFlags.NoSignalHandler;
                    Setup.GpioCfgSetInternals((ConfigFlags)config);
                    var initResultCode = Setup.GpioInitialise();
                    _initialized = initResultCode >= ResultCode.Ok;
                }
            }
        }



        public void RegisterPinControl(int pin, PinMode pinMode)
        {
            var gpio = (SystemGpio)pin;
            Pi.IO.GpioSetMode(gpio, pinMode);
        }

        public void UnregisterPinControl(int pin)
        {
            var gpio = (SystemGpio)pin;

            Pi.IO.GpioSetMode(gpio, PinMode.Input);
            ClearPinInterruptCallback(pin);
        }



        public void RegisterPinInterruptCallback(int pin, Action<int, LevelChange, uint> callback, EdgeDetection edgeDetection)
        {
            var gpio = (SystemGpio)pin;

            PiGpioIsrDelegate cb = new PiGpioIsrDelegate((gpioPin, levelChange, time) =>
            {
                callback((int)gpioPin, (LevelChange)levelChange, time);
            });
           Pi.IO.GpioSetIsrFunc(gpio,(EdgeDetection)edgeDetection, 0, cb);

        }
               
        public void ClearPinInterruptCallback(int pin)
        {
            var gpio = (SystemGpio)pin;
            Pi.IO.GpioSetIsrFunc(gpio, EdgeDetection.EitherEdge, 0, null);
        }




        public void Write(int pin, bool state)
        {
            if (!CanWrite(pin))
                throw new Exception($"Cannot write to pin {pin}. Writeable pings are 0 - 31");

            var gpio = (SystemGpio)pin;
            Pi.IO.GpioWrite(gpio, state);

        }



        public bool Read(int pin)
        {
            var gpio = (SystemGpio)pin;
            return Pi.IO.GpioRead(gpio);
        }





        private bool CanWrite(int pin)
        {
            return pin > 0 && pin <= 31;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Board.Release();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GpioService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
