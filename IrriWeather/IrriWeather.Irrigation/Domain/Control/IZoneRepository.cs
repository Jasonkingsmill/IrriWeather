using IrriWeather.Irrigation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Control
{
    public interface IZoneRepository : IRepository<Zone, int>
    {
    }
}
