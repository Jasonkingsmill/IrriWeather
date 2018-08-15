using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrriWeather.IO.Control.NativeEnums;
using IrriWeather.IO.Emulator;

namespace IrriWeather.IO
{
    public class RaspberryPiEmulationGpioService : IGpioService
    {
        private static object _lock = new object();
        private static List<int> _allocatedPins = new List<int>();

        private static Dictionary<int, MockGpio> _pins;

        public RaspberryPiEmulationGpioService()
        {
            lock (_lock)
            {
                if (_pins == null)
                {
                    _pins = new Dictionary<int, MockGpio>();
                    for (int i = 0; i < 32; i++)
                    {
                        RegisterPinControl(i, PinDirection.Input);
                    }
                }
            }
        }

        public IEnumerable<int> AllocatedPins { get => _allocatedPins; }


        public bool IsFreePin(int pin)
        {
            return AllocatedPins.Any(a => a == pin);
        }




        public void RegisterPinControl(int pin, PinDirection pinDirection)
        {
            if (!_pins.Any(x => x.Key == pin))
            {
                _pins.Add(pin, new MockGpio((SystemGpio)pin));
            }

            //if (!IsFreePin(pin))
            //    throw new ArgumentException($"Pin {pin} is already registered", nameof(pin));

            var gpio = _pins[pin];

            gpio.Direction = pinDirection;
        }

        public void UnregisterPinControl(int pin)
        {
            if (!_pins.Any(x => x.Key == pin))
            {
                _pins.Remove(pin);
            }
            ClearPinInterruptCallback(pin);
        }



        public void RegisterPinInterruptCallback(int pin, Action<int, LevelChange, uint> callback, EdgeDetection edgeDetection)
        {
            //if (IsFreePin(pin))
            //    throw new ArgumentException($"Pin {pin} has not been registered", nameof(pin));

            
        }



        public void ClearPinInterruptCallback(int pin)
        {
            //if (IsFreePin(pin))
            //    throw new ArgumentException($"Pin {pin} has not been registered", nameof(pin));


        }




        public void Write(int pin, bool state)
        {
            //if (IsFreePin(pin))
            //    throw new ArgumentException($"Pin {pin} has not been registered", nameof(pin));

            var gpio = _pins[pin];

            if (gpio.Mode != PinMode.Output)
                throw new ArgumentException($"Pin {pin} mode must first be set to output mode before attempting to write to pin", nameof(pin));

            gpio.Write(state ? 1 : 0);
        }



        public bool Read(int pin)
        {
            //if (IsFreePin(pin))
            //    throw new ArgumentException($"Pin {pin} has not been registered", nameof(pin));

            var gpio = _pins[pin];
            //if (gpio.Mode != PinMode.Input)
            //    throw new ArgumentException($"Pin {pin} mode must first be set to input mode before attempting to read pin", nameof(pin));

            return gpio.Value;
        }





        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

     

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

