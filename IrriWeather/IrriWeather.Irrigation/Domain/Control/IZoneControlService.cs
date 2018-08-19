using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IZoneControlService
    {
        bool IsStarted(int channel);
        void Start(int channel);
        void Stop(int channel);
        void Register(int channel);
        void Unregister(int channel);
    }
}
