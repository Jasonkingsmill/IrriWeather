using IrriWeather.Irrigation.Domain;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Scheduling;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IrriWeather.Irrigation.Infrastructure.Data
{
    public class IrrigationContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

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
            modelBuilder.Entity<Zone>(zone =>
            {
                zone.HasIndex(x => x.Channel).IsUnique(true);
            });

            modelBuilder.Entity<Schedule>(schedule =>
            {
                schedule.Property(x => x.ZoneIds).HasField("_zoneIds").HasConversion<string>(x => string.Join(";", x), 
                    x => x == null ? new HashSet<Guid>() : x.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(y => Guid.Parse(y)).ToHashSet());
                schedule.Property(x => x.Days).HasField("_days").HasConversion<string>(x => string.Join(";", x),
                    x => x == null ? new HashSet<int>() : x.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(y => Convert.ToInt32(y)).ToHashSet());
            });
        }
    }
}
