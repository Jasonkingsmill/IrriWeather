using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IrriWeather.Irrigation.Application.Schedule
{
    public class ZoneJob : IJob
    {
        public ZoneJob()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {

            var duration = (TimeSpan)context.JobDetail.JobDataMap.Get("duration");
            var zoneChannels = (IEnumerable<int>)context.JobDetail.JobDataMap.Get("zoneChannels");

            foreach (var channel in zoneChannels)
            {
                // enable board pin
            }

        }
    }
}
