using CloudWeather.Precipitation.Repositories;
using System;
using System.Threading.Tasks;

namespace CloudWeather.Precipitation.Services
{
    public class PrecipitationServices : IPrecipitationServices
    {
        private readonly IPrecipitationRepository _precipitationRepository;

        public PrecipitationServices(IPrecipitationRepository precipitationRepository)
        {
            _precipitationRepository = precipitationRepository;
        }

        public async Task CreateObservation(DataAccess.Precipitation precip)
        {
            await _precipitationRepository.CreateObservation(precip);
        }

        public async Task<System.Collections.Generic.List<CloudWeather.Precipitation.DataAccess.Precipitation>> GetPrecipitationByZipCode(string zip, DateTime dt)
        {
            return await _precipitationRepository.GetPrecipitationByZipCode(zip, dt);
        }
    }
}
