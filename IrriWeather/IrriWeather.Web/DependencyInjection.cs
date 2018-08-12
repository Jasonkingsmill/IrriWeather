using IrriWeather.IO;
using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Control;
using IrriWeather.Irrigation.Application.Schedule;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
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
            services.AddTransient<IChannelControlService, ChannelControlService>();
            services.AddTransient<ITriggerRepository, TriggerRepository>();
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddScoped<IChannelControlService, ChannelControlService>();

            services.AddTransient<RaspberryPiGpioService>();
            services.AddTransient<RaspberryPiEmulationGpioService>();
            services.AddScoped<IGpioService>(x =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return x.GetRequiredService<RaspberryPiEmulationGpioService>();
                else
                    return x.GetRequiredService<RaspberryPiGpioService>();

            });


            services.AddScoped<SchedulingService>();


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
