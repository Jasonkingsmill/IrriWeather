using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}
