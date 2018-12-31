using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class FactoryRepository : Repository<Factory, FactoryDTO>
    {
        private SiCContext context;

        public FactoryRepository(SiCContext context)
        {
            this.context = context;
        }

        public async Task<Factory> Add(FactoryDTO dto)
        {
            if (context.Factory.Any(f => f.Description == dto.Description)) return null;
            if (context.Factory.Any(f => f.City.CityId == dto.City.CityId)) return null;

            var city = context.City.Find(dto.City.CityId);

            if (city == null) return null;

            Factory factory = new Factory();
            factory.Description = dto.Description;
            factory.City = city;

            context.Factory.Add(factory);
            await context.SaveChangesAsync();

            return factory;
        }

        public async Task<Factory> Edit(int id, FactoryDTO dto)
        {
            var factory = await context.Factory.FindAsync(id);

            if (factory == null) return null;

            if (context.Factory.Any(f => f.Description == dto.Description)) return null;

            factory.Description = dto.Description;

            context.Entry(factory).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return factory;
        }

        public IEnumerable<Factory> FindAll()
        {
            return context.Factory;
        }

        public async Task<Factory> FindById(int id)
        {
            return await context.Factory.FindAsync(id);
        }

        public async Task<Factory> Remove(int id)
        {
            var factory = await context.Factory.FindAsync(id);

            if (factory == null) return null;

            context.Factory.Remove(factory);
            await context.SaveChangesAsync();

            return factory;
        }
    }
}