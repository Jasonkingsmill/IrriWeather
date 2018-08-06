using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Infrastructure.Data
{
    public class IrrigationContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Trigger> Triggers { get; set; }

        public IrrigationContext(string dataSource)
        {
            Environment.SetEnvironmentVariable("AppendManifestToken_SQLiteProviderManifest", ";BinaryGUID=True;");
            this.connectionString = dataSource;
            SQLitePCL.Batteries.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zone>().HasIndex(x => x.Channel).IsUnique(true);

            modelBuilder.Entity<DateTimeTrigger>();
            modelBuilder.Entity<DayOfMonthTrigger>(trigger =>
            {
                trigger.Ignore(x => x.Days);
                trigger.Property<string>("DaysOfMonth").HasField("_days");
            });
            modelBuilder.Entity<DayOfWeekTrigger>();
            modelBuilder.Entity<EvenDaysTrigger>();
            modelBuilder.Entity<OddDaysTrigger>();
        }
    }
}
