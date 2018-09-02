using IrriWeather.Irrigation.Domain.Control;
using IrriWeather.Irrigation.Domain.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrriWeather.Irrigation.Application.Scheduling
{
    public class ZoneJob : IJob, IDisposable
    {

        private readonly IServiceProvider container;
        private readonly IServiceScope scope;

        private readonly IScheduleRepository scheduleRepository;
        private readonly IZoneRepository zoneRepository;
        private readonly IZoneControlService controlService;

        public ZoneJob(IServiceProvider container)
        {            
            this.scope = container.CreateScope();
            this.scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
            this.zoneRepository = scope.ServiceProvider.GetRequiredService<IZoneRepository>();
            this.controlService = scope.ServiceProvider.GetRequiredService<IZoneControlService>();
        }

        public async Task Execute(IJobExecutionContext context)
        {

            var scheduleId = (Guid)context.JobDetail.JobDataMap.Get("scheduleId");

            var schedule = scheduleRepository.Find(scheduleId);
            var zones = zoneRepository.Find(schedule.ZoneIds);
                       
            foreach (var zone in zones)
            {
                controlService.Start(zone.Channel);
            }

            await Task.Delay(schedule.Duration);
            foreach (var zone in zones)
            {
                controlService.Stop(zone.Channel);
            }

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.scope.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ZoneJob() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
