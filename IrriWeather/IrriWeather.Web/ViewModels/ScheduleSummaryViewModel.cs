using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web.ViewModels
{
    public class ScheduleSummaryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScheduleType { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<int> Days { get; set; }

        public bool IsEnabled { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EnabledUntil { get; set; }
        public IEnumerable<Guid> ZoneIds { get; set; }
    }
}
