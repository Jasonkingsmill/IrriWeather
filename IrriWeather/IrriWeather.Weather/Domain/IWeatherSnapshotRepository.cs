using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Weather.Domain
{
    public interface IWeatherSnapshotRepository : IRepository<WeatherSnapshot, int>
    {
    }
}
