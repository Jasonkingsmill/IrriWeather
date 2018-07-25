using IrriWeather.Weather.Domain;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Weather.Data
{
    public class WeatherContext : DbContext
    {
        private readonly string dataSource;

        public DbSet<WeatherSnapshot> Snapshots { get; set; }

        public WeatherContext(string dataSource)
        {
            this.dataSource = dataSource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dataSource}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
