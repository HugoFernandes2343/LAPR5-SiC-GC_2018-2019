using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace SiC.Persistence
{
    public class CatalogRepository : Repository<Catalog, CatalogDTO>
    {

        private PersistenceContext context;

        public CatalogRepository(PersistenceContext context) 
        {
            this.context = context;
        }

        public IEnumerable<Catalog> FindAll()
        {
            return context.catalogs;
        }

        public async Task<Catalog> FindById(long id)
        {
           return await context.catalogs.FindAsync(id);
        }

        public async Task<Catalog> Edit(long id, CatalogDTO dto)
        {
           var catalog = await context.catalogs.FindAsync(id);

           if (catalog == null) return null;

           if (dto.Date != null) catalog.Date = dto.Date;

            context.Entry(catalog).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return catalog;

        }

        public async Task<Catalog> Remove(long id)
        {
           var catalog = await context.catalogs.FindAsync(id);
           if (catalog == null) return null;

           context.catalogs.Remove(catalog);
           await context.SaveChangesAsync();

           return catalog;

        }

        public async Task<Catalog> Add(CatalogDTO dto)
        {
            Catalog catalog = new Catalog();
            catalog.Date = dto.Date;
            context.catalogs.Add(catalog);
            await context.SaveChangesAsync();

            return catalog;
        }




    }
}