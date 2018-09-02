using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceProvider container;

        public JobFactory(IServiceProvider container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
