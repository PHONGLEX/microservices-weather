using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudWeather.Precipitation.Repositories
{
    public interface IPrecipitationRepository
    {
        Task<List<CloudWeather.Precipitation.DataAccess.Precipitation>> GetPrecipitationByZipCode(string zip, DateTime dt);
    }
}
