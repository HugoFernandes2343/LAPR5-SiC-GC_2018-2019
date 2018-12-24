using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class CatalogRepository : Repository<Catalog, CatalogDTO>
    {

        private SiCContext context;

        public CatalogRepository(SiCContext context)
        {
            this.context = context;
        }

        public async Task<Catalog> Add(CatalogDTO dto)
        {
            Catalog catalog = new Catalog();
            catalog.Date = dto.Date;
            context.Catalog.Add(catalog);
            await context.SaveChangesAsync();

            return catalog;
        }

        public async Task<Catalog> Edit(int id, CatalogDTO dto)
        {
            var catalog = await context.Catalog.FindAsync(id);

            if (catalog == null) return null;

            catalog.Date = dto.Date;

            context.Entry(catalog).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return catalog;
        }

        public IEnumerable<Catalog> FindAll()
        {
            return context.Catalog;
        }

        public async Task<Catalog> FindById(int id)
        {
            return await context.Catalog.FindAsync(id);
        }

        public async Task<Catalog> Remove(int id)
        {
            var catalog = await context.Catalog.FindAsync(id);
            
            if (catalog == null) return null;

            context.Catalog.Remove(catalog);
            await context.SaveChangesAsync();

            return catalog;
        }
    }
}