using IrriWeather.Irrigation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IrriWeather.Irrigation.Domain.Control;

namespace IrriWeather.Irrigation.Domain.Schedule
{
    public abstract class Trigger : Entity
    {
        private HashSet<Zone> zones = new HashSet<Zone>();

        protected Trigger()
        {
        }

        protected Trigger(TimeSpan duration, DateTime enabledUntil, bool isEnabled)
        {
            if (duration == null)
                throw new ArgumentNullException(nameof(duration));
            if (enabledUntil == null)
                throw new ArgumentNullException(nameof(enabledUntil));
            if (enabledUntil < DateTime.Now)
                throw new ArgumentException("EnabledUntil must be greater than now", nameof(enabledUntil));
            Duration = duration;
            EnabledUntil = enabledUntil;
            IsEnabled = isEnabled;
        }

        public TriggerType Type { get; protected set; }
        public bool IsEnabled { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public DateTime EnabledUntil { get; protected set; }
        public IEnumerable<Zone> Zones { get => zones.ToList(); set => zones = value.ToHashSet(); }

        public void SetEnablement(bool enabled)
        {
            IsEnabled = enabled;
        }

        public void AddTriggeredZone(Zone zone)
        {
            if (zone == null)
                throw new ArgumentNullException(nameof(zone));
            this.zones.Add(zone);
        }

        public abstract string BuildCronExpression();
    }
}
