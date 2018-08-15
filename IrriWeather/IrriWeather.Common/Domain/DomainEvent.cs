using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{

    public abstract class DomainEvent
    {
        public int EventVersion { get; protected set; } = 1;
        public DateTime OccurredOn { get; protected set; } = DateTime.Now;

    }
}
