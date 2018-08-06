using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IChannelControlService
    {
        bool GetState(int channel);
        void SetState(int channel, bool on);
    }
}
