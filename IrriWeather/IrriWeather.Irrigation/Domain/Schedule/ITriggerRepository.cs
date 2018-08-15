using System;
using System.Collections.Generic;
using System.Text;
using IrriWeather.Common.Domain;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public interface ITriggerRepository : IRepository<Trigger, Guid>
    {
    }
}
