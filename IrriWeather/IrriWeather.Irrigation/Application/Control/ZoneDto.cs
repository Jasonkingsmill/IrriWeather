using System;

namespace IrriWeather.Irrigation.Application.Control
{
    public class ZoneDto
    {
        public ZoneDto(Guid id, string name, string description, int channel, bool isEnabled)
        {
            Id = id;
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
        }

        public Guid Id { get; }
        public string Name { get;  }
        public string Description { get; }
        public int Channel { get; }
        public bool IsEnabled { get;  }
    }
}
