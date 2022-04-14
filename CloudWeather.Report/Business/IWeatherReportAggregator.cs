using CloudWeather.Report.DataAccess;
using System.Threading.Tasks;

namespace CloudWeather.Report.Business
{
    public interface IWeatherReportAggregator
    {
        Task<WeatherReport> BuildWeeklyReport(string zip, int days);
    }
}
