using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Data
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IrrigationContext context;

        public ScheduleRepository(IrrigationContext context)
        {
            this.context = context;
        }

        public void Add(Schedule entity)
        {
            context.Schedules.Add(entity);
            context.SaveChanges();
        }

        public void Update(Schedule entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }


        public Schedule Find(Guid id)
        {
            return context.Schedules.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Schedule> FindAll()
        {
            return context.Schedules.ToList();
        }

        public void Remove(Schedule entity)
        {
            context.Schedules.Remove(entity);
            context.SaveChanges();
        }
    }
}
