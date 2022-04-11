using CloudWeather.Precipitation.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudWeather.Precipitation.Repositories
{
    public class PrecipitationRepository : IPrecipitationRepository
    {
        private readonly PrecipDbContext _context;

        public PrecipitationRepository(PrecipDbContext context)
        {
            _context = context;
        }

        public async Task<List<CloudWeather.Precipitation.DataAccess.Precipitation>> GetPrecipitationByZipCode(string zip, DateTime dt)
        {
            return await _context.Precipitation.Where(precip => precip.ZipCode == zip && precip.CreatedOn > dt).ToListAsync();
        }
    }
}
