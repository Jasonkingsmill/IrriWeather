using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web.ViewModels
{
    public class AddScheduleViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScheduleType { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public List<int> Days { get; set; }

        public bool IsEnabled { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EnabledUntil { get; set; }
        public List<Guid> ZoneIds { get; set; }
    }
}
