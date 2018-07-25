using IrriWeather.Irrigation.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.PiGpio;
using System.Linq;

namespace IrriWeather.Irrigation.Application
{
    public class ChannelStateService : IChannelStateService
    {
        public ChannelStateService()
        {
        }

        public void ChangeState(int channel, bool newState)
        {
            Board.Pins[channel].Value = newState;
        }

        public bool GetState(int channel)
        {
            return Board.Pins[channel].Value;
        }

        public void InitialiseChannels(List<int> channels)
        {
            foreach (var pin in Board.Pins.Where(x => channels.Contains(x.Value.PinNumber)).Select(x => x.Value))
            {
                pin.Direction = Unosquare.PiGpio.NativeEnums.PinDirection.Output;
                pin.Value = false;
            }
        }



    }
}
