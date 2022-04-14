using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudWeather.Temperature.Repositories
{
    public interface ITemperatureRepository
    {
        Task<List<CloudWeather.Temperature.DataAccess.Temperature>> GetObservationByZipCode(string zip, DateTime dt);

        Task CreateObservation(CloudWeather.Temperature.DataAccess.Temperature precip);
    }
}
