using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Control;
using IrriWeather.Irrigation.Application.Schedule;
using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Schedule;
using IrriWeather.Irrigation.Infrastructure.Control;
using IrriWeather.Irrigation.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web
{
    public class DependencyInjection
    {
        public static IServiceCollection GetServices(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ZoneJob>();

            services.AddTransient<JobFactory>(x=> new JobFactory(x));
            services.AddSingleton<SchedulingService>();

            services.AddScoped<IrrigationContext>(x=> new IrrigationContext(configuration.GetConnectionString("Irrigation")));

            services.AddTransient<ZoneService>();
            services.AddTransient<IChannelControlService, ChannelControlService>();
            services.AddTransient<ITriggerRepository, TriggerRepository>();
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddSingleton<IChannelControlService, ChannelControlService>();

            return services;
        }
    }
}
