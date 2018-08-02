using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IZoneControlService
    {
        bool GetState(int channel);
        void SetState(int channel, bool on);
    }
}
