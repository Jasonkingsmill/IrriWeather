using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Schedule;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Data
{
    public class IrrigationContext : DbContext
    {
        private readonly string dataSource;

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Trigger> Triggers { get; set; }

        public IrrigationContext(string dataSource)
        {
            this.dataSource = dataSource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dataSource}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zone>().HasIndex(x => x.Channel).IsUnique(true);


            modelBuilder.Entity<DayOfWeekTrigger>();
        }
    }
}
