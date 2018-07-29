using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IZoneService
    {
        bool GetState(int channel);
        void SetState(int channel, bool on);
    }
}
