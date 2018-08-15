using System;
using System.Collections.Generic;
using System.Text;
using IrriWeather.Common.Domain;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IZoneRepository : IRepository<Zone, Guid>
    {
    }
}
