using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Weather.Domain
{
    public class WeatherSnapshot
    {
        public WeatherSnapshot(Wind wind, Rain rain, Temperature temperature, Pressure pressure, Humidity humidity)
        {
            Wind = wind;
            Rain = rain;
            Temperature = temperature;
            Pressure = pressure;
            Humidity = humidity;
        }

        public Wind Wind { get; }
        public Rain Rain { get; }
        public Temperature Temperature { get; }
        public Pressure Pressure { get; }
        public Humidity Humidity { get; }
    }
}
