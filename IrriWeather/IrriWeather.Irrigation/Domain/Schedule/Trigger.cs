using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public abstract class Trigger : Entity
    {
        protected Trigger()
        {
        }

        public bool IsEnabled { get; protected set; }
        public TriggerType Type { get; protected set; }
    }
}
