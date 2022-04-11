using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudWeather.Precipitation.Services
{
    public interface IPrecipitationServices
    {
        Task<List<CloudWeather.Precipitation.DataAccess.Precipitation>> GetPrecipitationByZipCode(string zip, DateTime dt);
    }
}
