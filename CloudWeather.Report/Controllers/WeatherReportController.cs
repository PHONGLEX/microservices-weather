using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudWeather.Report.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudWeather.Report.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherReportController : ControllerBase
    {
        private readonly ILogger<WeatherReportController> _logger;
        private readonly IWeatherReportAggregator _weatherReportAggregator;

        public WeatherReportController(ILogger<WeatherReportController> logger, IWeatherReportAggregator weatherReportAggregator)
        {
            _logger = logger;
            _weatherReportAggregator = weatherReportAggregator;
        }

        [HttpGet("{zip}")]
        public async Task<IActionResult> BuildReport(string zip, [FromQuery] int? days)
        {
            if (days == null || days > 30  || days < 1)
            {
                return BadRequest("Please provide a 'days' query parameter with a value between 1 and 30");  
            }
            var result = await _weatherReportAggregator.BuildWeeklyReport(zip, days.Value);
            return Ok(result);
        }
    }
}
