using System;

namespace IrriWeather.Irrigation.Application.Models
{
    public class ZoneModel
    {
        public ZoneModel(string name, string description, int channel, bool isEnabled)
        {
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
        }

        public string Name { get;  }
        public string Description { get; }
        public int Channel { get; }
        public bool IsEnabled { get;  }
    }
}
