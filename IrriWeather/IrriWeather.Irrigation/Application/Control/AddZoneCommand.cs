using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Control
{
    public class AddZoneCommand
    {
        public AddZoneCommand(string name, string description, int channel, bool isEnabled)
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
