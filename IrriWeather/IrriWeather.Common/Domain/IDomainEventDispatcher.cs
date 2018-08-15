using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}
