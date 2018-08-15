using System;
using System.Collections.Generic;
using System.Text;
using IrriWeather.Common.Application;

namespace IrriWeather.Irrigation.Application.Control
{
    public class ZoneRemovedEvent : IApplicationEvent
    {
        public ZoneRemovedEvent(Guid zoneId, int channel)
        {
            OccurredOn = DateTime.Now;
            ZoneId = zoneId;
            Channel = channel;
        }

        public DateTime OccurredOn { get; private set; }
        public Guid ZoneId { get; }
        public int Channel { get; }
    }
}
