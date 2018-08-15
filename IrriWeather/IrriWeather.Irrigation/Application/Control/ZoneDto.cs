using System;

namespace IrriWeather.Irrigation.Application.Control
{
    public class ZoneDto
    {
        public ZoneDto(Guid id, string name, string description, int channel, bool isEnabled, bool isStarted)
        {
            Id = id;
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
            IsStarted = isStarted;
        }

        public Guid Id { get; }
        public string Name { get;  }
        public string Description { get; }
        public int Channel { get; }
        public bool IsEnabled { get;  }
        public bool IsStarted { get; }
    }
}
