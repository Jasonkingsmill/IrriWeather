using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Application
{
    public interface IApplicationEventDispatcher
    {
        void Dispatch<TEvent>(TEvent applicationEvent) where TEvent : IApplicationEvent;
    }
}
