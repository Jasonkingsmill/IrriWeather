using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using IrriWeather.Irrigation.Application.Scheduling;
using Quartz;
using Quartz.Impl;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Domain.Control;
using System.Diagnostics;
using System.Reflection;

namespace IrriWeather.Web
{
    public class Program
    {

        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var applicationPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);

            if (Debugger.IsAttached)
            {
                applicationPath = Directory.GetCurrentDirectory();
            }

            var builder = new ConfigurationBuilder()
            .SetBasePath(applicationPath)
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var services = DependencyInjection.GetServices(Configuration);

            var container = services.BuildServiceProvider();
            using (var scope = container.CreateScope())
            {
                var schedulerService = container.GetRequiredService<ScheduleService>();
                schedulerService.InitializeScheduler();

                var zoneRepo = container.GetService<IZoneRepository>();
                var controlService = container.GetService<IZoneControlService>();
                var zones = zoneRepo.FindAll();
                if (zones != null)
                {
                    foreach (var zone in zones)
                    {
                        controlService.Register(zone.Channel);
                    }
                }
            }

            var host = BuildWebHost(args, services, applicationPath);


            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                Console.WriteLine("Process exiting...");
                Console.WriteLine("Stopping all zones...");
                var zoneRepo = container.GetService<IZoneRepository>();
                var controlService = container.GetService<IZoneControlService>();
                var zones = zoneRepo.FindAll();
                if (zones != null)
                {
                    foreach (var zone in zones)
                    {
                        controlService.Stop(zone.Channel);
                    }
                }
            };

            host.Run();
        }


        public static IWebHost BuildWebHost(string[] args, IServiceCollection services, string applicationPath) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((s) =>
                {
                    foreach (var service in services)
                    {
                        s.Add(service);
                    }
                })
                .UseContentRoot(applicationPath)
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();
    }
}
