using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudWeather.Precipitation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudWeather.Precipitation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecipitationController : ControllerBase
    {
        
        private readonly ILogger<PrecipitationController> _logger;
        private readonly IPrecipitationServices _services;

        public PrecipitationController(ILogger<PrecipitationController> logger, IPrecipitationServices services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet("{zip}")]
        public ActionResult<List<CloudWeather.Precipitation.DataAccess.Precipitation>> Get(string zip, [FromQuery] int? days)
        {
            if (days == null || days < 0 || days > 30)
            {
                return BadRequest("Please provide a 'days' query parameter between 1 and 30");
            }

            var startDate = DateTime.UtcNow - TimeSpan.FromDays(days.Value);

            var results = _services.GetPrecipitationByZipCode(zip, startDate);

            return Ok(results);
        }
    }
}
