using CloudWeather.Temperature.Repositories;
using System;
using System.Threading.Tasks;

namespace CloudWeather.Temperature.Services
{
    public class TemperatureServices : ITemperatureServices
    {
        private readonly ITemperatureRepository _precipitationRepository;

        public TemperatureServices(ITemperatureRepository precipitationRepository)
        {
            _precipitationRepository = precipitationRepository;
        }

        public async Task CreateObservation(DataAccess.Temperature precip)
        {
            await _precipitationRepository.CreateObservation(precip);
        }

        public async Task<System.Collections.Generic.List<CloudWeather.Temperature.DataAccess.Temperature>> GetObservationByZipCode(string zip, DateTime dt)
        {
            return await _precipitationRepository.GetObservationByZipCode(zip, dt);
        }
    }
}
