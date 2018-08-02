using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web.ViewModels
{
    public class ZoneSummaryViewModel
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Channel { get; private set; }
        public bool IsEnabled { get; private set; }
    }
}
