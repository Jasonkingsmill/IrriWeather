using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Control
{
    public class RemoveZoneCommand
    {
        public RemoveZoneCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
