using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrriWeather.Web.ViewModels
{
    public class AddZoneViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Channel { get; set; }
        public bool IsEnabled { get; set; }
    }
}
