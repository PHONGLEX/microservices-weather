using CloudWeather.Temperature.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudWeather.Temperature.Repositories
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly TemperatureDbContext _context;

        public TemperatureRepository(TemperatureDbContext context)
        {
            _context = context;
        }

        public async Task CreateObservation(DataAccess.Temperature precip)
        {
            await _context.AddAsync(precip);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CloudWeather.Temperature.DataAccess.Temperature>> GetObservationByZipCode(string zip, DateTime dt)
        {
            return await _context.Temperature.Where(precip => precip.ZipCode == zip && precip.CreatedOn > dt).ToListAsync();
        }
    }
}
