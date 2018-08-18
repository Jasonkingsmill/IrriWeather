using IrriWeather.IO.Control.NativeEnums;
using System;
using System.Collections.Generic;

namespace IrriWeather.IO
{
    public interface IGpioService
    {
        void ClearPinInterruptCallback(int pin);
        void Dispose();
        //bool IsFreePin(int pin);
        bool Read(int pin);
        void RegisterPinControl(int pin, PinMode mode);
        void UnregisterPinControl(int pin);
        void RegisterPinInterruptCallback(int pin, Action<int, LevelChange, uint> callback, EdgeDetection edgeDetection);
        void Write(int pin, bool state);
    }
}