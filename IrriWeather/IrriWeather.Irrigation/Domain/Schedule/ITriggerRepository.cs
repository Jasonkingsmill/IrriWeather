using IrriWeather.Irrigation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public interface ITriggerRepository : IRepository<Trigger, Guid>
    {
    }
}
