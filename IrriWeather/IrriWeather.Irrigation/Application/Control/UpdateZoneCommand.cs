using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Control
{
    public class UpdateZoneCommand
    {
        public UpdateZoneCommand(Guid id, string name, string description, int channel, bool isEnabled)
        {
            Id = id;
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Channel { get; private set; }
        public bool IsEnabled { get; private set; }
    }
}
