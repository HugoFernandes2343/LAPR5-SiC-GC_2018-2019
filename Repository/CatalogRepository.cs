using System.Collections.Generic;
using System.Linq;
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
            if(context.Catalog.Any(c => c.CatalogName == dto.CatalogName)) return null;

            Catalog catalog = new Catalog();
            catalog.CatalogName = dto.CatalogName;
            catalog.CatalogDescription = dto.CatalogDescription;
            catalog.Date = dto.Date;
            catalog.CatalogProducts = new List<CatalogProduct>();
            context.Catalog.Add(catalog);
            await context.SaveChangesAsync();

            return catalog;
        }

        public async Task<Catalog> Edit(int id, CatalogDTO dto)
        {
            var catalog = await context.Catalog.FindAsync(id);

            if (catalog == null) return null;

            if (context.Catalog.Any(c => c.CatalogName == dto.CatalogName && c.CatalogId != id)) return null;

            catalog.CatalogName = dto.CatalogName;
            catalog.CatalogDescription = dto.CatalogDescription;
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

            foreach (CatalogProduct cp in catalog.CatalogProducts)
            {
                context.CatalogProduct.Remove(cp);
            }

            context.Catalog.Remove(catalog);
            await context.SaveChangesAsync();

            return catalog;
        }

        public async Task<Catalog> AddCatalogProduct(int id, int idp)
        {
            
            var catalog = await context.Catalog.FindAsync(id);
            var product = await context.Product.FindAsync(idp);

            if (product == null || catalog == null) return null;

            if (context.CatalogProduct.Any(cps => cps.ProductId == idp && cps.CatalogId == id)) return null;

            CatalogProduct cp = new CatalogProduct();
            cp.ProductId = product.ProductId;
            cp.Product = product;
            cp.CatalogId= catalog.CatalogId;
            cp.Catalog = catalog;

            product.CatalogProducts.Add(cp);
            catalog.CatalogProducts.Add(cp);

            context.Entry(catalog).State = EntityState.Modified;
            context.Entry(product).State = EntityState.Modified;
            context.CatalogProduct.Add(cp);

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

        public async Task<Catalog> RemoveCatalogProduct(int id, int idp)
        {
            var catalog = await context.Catalog.FindAsync(id);
            var product = await context.Product.FindAsync(idp);
          
            CatalogProduct cps = null;

            if (product == null || catalog == null) return null;

            foreach(CatalogProduct cp in catalog.CatalogProducts){
                if(cp.ProductId == idp){
                    cps = cp;
                }
            }

            if (cps == null) return null;

            context.CatalogProduct.Remove(cps);
            await context.SaveChangesAsync();

            return catalog;
        }
    }
}