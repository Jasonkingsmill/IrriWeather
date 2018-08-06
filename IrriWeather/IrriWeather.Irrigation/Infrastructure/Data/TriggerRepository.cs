using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Data
{
    public class TriggerRepository : ITriggerRepository
    {
        private readonly IrrigationContext context;

        public TriggerRepository(IrrigationContext context)
        {
            this.context = context;
        }

        public void Add(Trigger entity)
        {
            context.Triggers.Add(entity);
            context.SaveChanges();
        }

        public Trigger Find(Guid id)
        {
            return context.Triggers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Trigger> FindAll()
        {
            return context.Triggers.ToList();
        }

        public void Remove(Trigger entity)
        {
            context.Triggers.Remove(entity);
            context.SaveChanges();
        }
    }
}
