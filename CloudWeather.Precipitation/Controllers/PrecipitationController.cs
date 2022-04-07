using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudWeather.Precipitation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecipitationController : ControllerBase
    {
        
        private readonly ILogger<PrecipitationController> _logger;

        public PrecipitationController(ILogger<PrecipitationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{zip}")]
        public string Get(string zip, [FromQuery] int? days)
        {
            return zip;
        }
    }
}
