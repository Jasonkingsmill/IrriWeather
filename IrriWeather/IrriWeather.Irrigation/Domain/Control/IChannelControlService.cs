using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IChannelControlService
    {
        bool IsStarted(int channel);
        void Start(int channel);
        void Stop(int channel);
        void Initialise(int channel);
    }
}
