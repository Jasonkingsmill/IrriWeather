using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IrriWeather.Web.Controllers
{
    [Route("api/[controller]")]
    public class IrrigationController : Controller
    {
      
        [HttpGet("[action]")]
        public IEnumerable<string> GetValves()
        {
            return new List<string>();
        }

    }
}
