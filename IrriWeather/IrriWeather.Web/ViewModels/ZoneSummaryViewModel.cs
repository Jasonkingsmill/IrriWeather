using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web.ViewModels
{
    public class ZoneSummaryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Channel { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsStarted { get; set; }
    }
}
