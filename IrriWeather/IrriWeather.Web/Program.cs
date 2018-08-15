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
using IrriWeather.Irrigation.Application.Schedule;
using Quartz;
using Quartz.Impl;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Domain.Control;

namespace IrriWeather.Web
{
    public class Program
    {

        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {


            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var services = DependencyInjection.GetServices(Configuration);

            var container = services.BuildServiceProvider();
            using (var scope = container.CreateScope())
            {
                var schedulerService = container.GetRequiredService<SchedulingService>();
                schedulerService.InitializeScheduler();

                var zoneRepo = container.GetService<IZoneRepository>();
                var channelService = container.GetService<IChannelControlService>();
                var zones = zoneRepo.FindAll();
                foreach(var zone in zones)
                {
                    channelService.Register(zone.Channel);
                }
            }

            var host = BuildWebHost(args, services);


            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args, IServiceCollection services) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((s) =>
                {
                    foreach (var service in services)
                    {
                        s.Add(service);
                    }
                })
                .UseStartup<Startup>()
                .Build();
    }
}
