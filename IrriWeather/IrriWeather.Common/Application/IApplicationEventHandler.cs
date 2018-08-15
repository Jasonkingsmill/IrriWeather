using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Application
{
    public interface IApplicationEventHandler<T> where T : IApplicationEvent
    {
        void Handle(T @event);
    }
}
