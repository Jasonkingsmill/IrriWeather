using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain
{
    public interface IChannelStateService
    {
        void ChangeState(int channel, bool newState);
        bool GetState(int channel);
        void InitialiseChannels(List<int> channels);
    }
}
