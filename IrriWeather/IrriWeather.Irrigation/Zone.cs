using System;

namespace IrriWeather.Irrigation
{
    public class Zone
    {
        public Zone(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
        public string GetStateDescription()
        {
            return "Unknown";
        }
    }
}
