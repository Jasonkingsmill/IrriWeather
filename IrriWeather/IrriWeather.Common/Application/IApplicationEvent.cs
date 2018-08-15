using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Application
{
    public interface IApplicationEvent
    {
        DateTime OccurredOn { get; }
    }
}
