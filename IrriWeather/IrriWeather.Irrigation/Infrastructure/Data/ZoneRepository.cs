using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Data
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly IrrigationContext context;

        public ZoneRepository(IrrigationContext context)
        {
            this.context = context;
        }

        public void Add(Zone entity)
        {
            context.Zones.Add(entity);
            context.SaveChanges();
        }

        public void Update(Zone entity)
        {
            context.Update(entity);                        
            context.SaveChanges();
        }

        public Zone Find(Guid id)
        {
            return context.Zones.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Zone> FindAll()
        {
            return context.Zones.ToList();
        }

        public void Remove(Zone entity)
        {
            context.Zones.Remove(entity);
            context.SaveChanges();
        }
    }
}
