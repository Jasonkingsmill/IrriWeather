using IrriWeather.Irrigation.Application;
using IrriWeather.Irrigation.Application.Schedule;
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
        public static IServiceCollection GetServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ZoneJob>();

            services.AddTransient<JobFactory>(x=> new JobFactory(x));
            services.AddSingleton<SchedulingService>();

            return services;
        }
    }
}
