using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
