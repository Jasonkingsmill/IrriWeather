using System;
using System.Collections.Generic;
using System.Text;
using IrriWeather.Common.Domain;

namespace IrriWeather.Weather.Domain
{
    public interface IWeatherSnapshotRepository : IRepository<WeatherSnapshot, int>
    {
    }
}
