using System;
using System.Collections.Generic;
using System.Text;
using IrriWeather.Common.Domain;

namespace IrriWeather.Irrigation.Domain.Scheduling
{
    public interface IScheduleRepository : IRepository<Schedule, Guid>
    {
    }
}
