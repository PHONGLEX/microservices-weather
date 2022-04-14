using CloudWeather.Report.Config;
using CloudWeather.Report.DataAccess;
using CloudWeather.Report.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudWeather.Report.Business
{
    public class WeatherReportAggregator : IWeatherReportAggregator 
    {
        private readonly IHttpClientFactory _http;
        private readonly ILogger<IHttpClientFactory> _logger;
        private readonly WeatherDataConfig _weatherDataConfig;
        private readonly WeatherReportDbContext _db;

        public WeatherReportAggregator(IHttpClientFactory http, ILogger<IHttpClientFactory> logger, IOptions<WeatherDataConfig> weatherDataConfig, WeatherReportDbContext db)
        {
            _http = http;
            _logger = logger;
            _weatherDataConfig = weatherDataConfig.Value;
            _db = db;
        }

        public async Task<WeatherReport> BuildWeeklyReport(string zip, int days)
        {
            var httpClient = _http.CreateClient();
            var precipData = await FetchPrecipitationData(httpClient, zip, days);
            var totalSnow = GetTotalSnow(precipData);
            var totalRain = GetTotalRain(precipData);

            var tempData = await FetchTemperatureData(httpClient, zip, days);
            var avatageHighTemp = tempData.Average(t => t.TempHighF);
            var avatageLowTemp = tempData.Average(t => t.TempLowF);

            var weeklyWeatherReport = new WeatherReport
            {
                AverageHighF = Math.Round(avatageHighTemp, 1),
                AverageLowF = Math.Round(avatageLowTemp, 1),
                RainfallTotalInches = totalRain,
                SnowTotalInches = totalSnow,
                ZipCode = zip,
                CreatedOn = DateTime.UtcNow,
            };
            _db.Add(weeklyWeatherReport);
            await _db.SaveChangesAsync();

            return weeklyWeatherReport;
        }

        private static decimal GetTotalSnow(IEnumerable<PrecipitationModel> precipData)
        {
            var totalSnow = precipData.Where(p => p.WeatherType == "snow").Sum(p => p.AmountInches);
            return Math.Round(totalSnow, 1);
        }

        private static decimal GetTotalRain(IEnumerable<PrecipitationModel> precipData)
        {
            var totalSnow = precipData.Where(p => p.WeatherType == "rain").Sum(p => p.AmountInches);
            return Math.Round((decimal)totalSnow, 1);
        }

        private async Task<List<TemperatureModel>> FetchTemperatureData(HttpClient httpClient, string zip, int days)
        {
            var endpoint = BuildTemperatureServiceEndpoint(zip, days);
            var temperatureRecords = await httpClient.GetAsync(endpoint);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var temperatureData = await temperatureRecords.Content.ReadFromJsonAsync<List<TemperatureModel>>(jsonSerializerOptions);
            return temperatureData ?? new List<TemperatureModel>();
        }

        private async Task<List<PrecipitationModel>> FetchPrecipitationData(HttpClient httpClient, string zip, int days)
        {
            var endpoint = BuildPrecipitationServiceEndpoint(zip, days);
            var precipRecords = await httpClient.GetAsync(endpoint);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var precipData = await precipRecords.Content.ReadFromJsonAsync<List<PrecipitationModel>>(jsonSerializerOptions);
            return precipData ?? new List<PrecipitationModel>();
        }

        private string BuildTemperatureServiceEndpoint(string zip, int days)
        {
            var tempServiceProtocol = _weatherDataConfig.TempDataProtocol;
            var tempServiceHost = _weatherDataConfig.TempDataHost;
            var tempServicePort = _weatherDataConfig.TempDataPort;
            return $"{tempServiceProtocol}://{tempServiceHost}:{tempServicePort}/temperature/observation/{zip}?days={days}";
        }

        private string BuildPrecipitationServiceEndpoint(string zip, int days)
        {
            var precipServiceProtocol = _weatherDataConfig.PrecipDataProtocol;
            var precipServiceHost = _weatherDataConfig.PrecipDataHost;
            var precipServicePort = _weatherDataConfig.PrecipDataPort;
            return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/precipitation/observation/{zip}?days={days}";
        }
    }
}
