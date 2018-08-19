using IrriWeather.IO;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Control;
using IrriWeather.Irrigation.Application.Scheduling;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Scheduling;
using IrriWeather.Irrigation.Infrastructure.Control;
using IrriWeather.Irrigation.Infrastructure.Data;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace IrriWeather.Web
{
    public class DependencyInjection
    {
        public static IServiceCollection GetServices(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ZoneJob>();

            services.AddTransient<JobFactory>(x => new JobFactory(x));

            services.AddScoped<IrrigationContext>(x => new IrrigationContext(configuration.GetConnectionString("Irrigation")));

            services.AddTransient<ZoneService>();
            services.AddTransient<IZoneControlService, ZoneControlService>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddScoped<IZoneControlService, ZoneControlService>();

            services.AddSingleton<RaspberryPiGpioService>();
            services.AddSingleton<RaspberryPiEmulationGpioService>();
            services.AddScoped<IGpioService>(x =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return x.GetRequiredService<RaspberryPiEmulationGpioService>();
                else
                    return x.GetRequiredService<RaspberryPiGpioService>();

            });


            services.AddScoped<ScheduleService>();


            services.AddSingleton<IScheduler>(x =>
            {
                var scheduler = new StdSchedulerFactory().GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
                scheduler.JobFactory = x.GetRequiredService<JobFactory>();
                return scheduler;
            });

            return services;
        }
    }
}
