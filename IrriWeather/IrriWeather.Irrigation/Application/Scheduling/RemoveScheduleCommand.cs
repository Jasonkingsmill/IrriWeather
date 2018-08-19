using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class RemoveScheduleCommand
    {
        public RemoveScheduleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
