using IrriWeather.Irrigation.Domain.Common;
using System;

namespace IrriWeather.Irrigation.Domain.Control
{
    public class Zone : Entity
    {
        public Zone(string name, string description, int channel, bool isEnabled)
        {
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Channel { get; private set; }
        public bool IsEnabled { get; private set; }
        
    }
}
