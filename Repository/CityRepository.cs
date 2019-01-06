using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class CityRepository : Repository<City, CityDTO>
    {
        private SiCContext context;

        public CityRepository(SiCContext context)
        {
            this.context = context;
        }

        public async Task<City> Add(CityDTO dto)
        {
            if (context.City.Any(c => c.Name == dto.Name))
            {
                return null;
            }
            if (context.City.Any(c => c.Latitude == dto.Latitude && c.Longitude == dto.Longitude))
            {
                return null;
            }

            City city = new City();
            city.Name = dto.Name;
            city.Latitude = dto.Latitude;
            city.Longitude = dto.Longitude;

            context.City.Add(city);
            await context.SaveChangesAsync();

            return city;
        }

        public async Task<City> Edit(int id, CityDTO dto)
        {
            var city = await context.City.FindAsync(id);

            if (city == null) return null;

            if (context.City.Any(c => c.Name == dto.Name && c.CityId != dto.CityId)) return null;

            if (context.City.Any(c => c.Latitude == dto.Latitude && c.Longitude == dto.Longitude && c.CityId != dto.CityId)) return null;

            city.Name = dto.Name;
            city.Latitude = dto.Latitude;
            city.Longitude = dto.Longitude;

            context.Entry(city).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return city;
        }

        public IEnumerable<City> FindAll()
        {
            return context.City;
        }

        public async Task<City> FindById(int id)
        {
            return await context.City.FindAsync(id);
        }

        public async Task<City> Remove(int id)
        {
            var city = await context.City.FindAsync(id);

            if (city == null) return null;

            context.City.Remove(city);
            await context.SaveChangesAsync();

            return city;
        }
    }
}