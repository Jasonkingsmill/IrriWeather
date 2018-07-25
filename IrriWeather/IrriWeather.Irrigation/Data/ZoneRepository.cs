﻿using IrriWeather.Irrigation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Data
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

        public Zone Find(int id)
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
