using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudWeather.Temperature.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudWeather.Temperature.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;
        private readonly ITemperatureServices _services;

        public TemperatureController(ILogger<TemperatureController> logger, ITemperatureServices services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet("observation/{zip}")]
        public async Task<ActionResult<List<CloudWeather.Temperature.DataAccess.Temperature>>> Get(string zip, [FromQuery] int? days)
        {
            if (days == null || days < 0 || days > 30)
            {
                return BadRequest("Please provide a 'days' query parameter between 1 and 30");
            }

            var startDate = DateTime.UtcNow - TimeSpan.FromDays(days.Value);

            var results = await _services.GetObservationByZipCode(zip, startDate);

            return Ok(results);
        }

        [HttpPost("observation")]
        public async Task Post(CloudWeather.Temperature.DataAccess.Temperature temperature)
        {
            temperature.CreatedOn = temperature.CreatedOn.ToUniversalTime();
            await _services.CreateObservation(temperature);
        }
    }
}
