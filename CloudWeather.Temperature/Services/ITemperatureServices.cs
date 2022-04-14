using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudWeather.Temperature.Services
{
    public interface ITemperatureServices
    {
        Task<List<CloudWeather.Temperature.DataAccess.Temperature>> GetObservationByZipCode(string zip, DateTime dt);

        Task CreateObservation(DataAccess.Temperature precip);
    }
}
