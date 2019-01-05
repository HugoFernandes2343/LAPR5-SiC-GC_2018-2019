using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiC.Repository
{
    public class CollectionRepository : Repository<Collection, CollectionDTO>
    {
        private SiCContext context;

        public CollectionRepository(SiCContext context)
        {
            this.context = context;
        }

        public async Task<Collection> Add(CollectionDTO dto)
        {
            if (context.Collection.Any(m => m.collectionName == dto.collectionName)) return null;

            Collection collection = new Collection();
            collection.collectionName = dto.collectionName;
            collection.aestheticParameter = dto.aestheticParameter;
            collection.CollectionProducts = new List<CollectionProduct>();

            context.Collection.Add(collection);

            await context.SaveChangesAsync();

            return collection;
        }

        public async Task<Collection> Edit(int id, CollectionDTO dto)
        {
            var collection = await context.Collection.FindAsync(id);

            if (collection == null) { return null; }

            if (context.Collection.Any(c => c.collectionName == dto.collectionName && c.CollectionId != id)) return null;

            collection.collectionName = dto.collectionName;
            collection.aestheticParameter = dto.aestheticParameter;
            context.Entry(collection).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return collection;
        }

        public IEnumerable<Collection> FindAll()
        {
            return context.Collection;
        }

        public async Task<Collection> FindById(int id)
        {
            return await context.Collection.FindAsync(id);
        }

        public async Task<Collection> Remove(int id)
        {
            var collection = await context.Collection.FindAsync(id);
            if (collection == null) return null;

            foreach (CollectionProduct cp in collection.CollectionProducts)
            {
                context.CollectionProduct.Remove(cp);
            }

            context.Collection.Remove(collection);
            await context.SaveChangesAsync();

            return collection;
        }

        public async Task<Collection> AddCollectionProduct(int id, int idp)
        {
            var collection = await context.Collection.FindAsync(id);
            var product = await context.Product.FindAsync(idp);

            if (product == null || collection == null) return null;

            if (context.CollectionProduct.Any(cps => cps.ProdutctId == idp && cps.CollectionId == id)) return null;

            CollectionProduct cp = new CollectionProduct();
            cp.ProdutctId = product.ProductId;
            cp.Product = product;
            cp.Collection = collection;
            cp.CollectionId = collection.CollectionId;

            product.CollectionProducts.Add(cp);
            collection.CollectionProducts.Add(cp);

            context.Entry(collection).State = EntityState.Modified;
            context.Entry(product).State = EntityState.Modified;
            context.CollectionProduct.Add(cp);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return collection;
        }

        public async Task<Collection> RemoveCollectionProduct(int id, int idp)
        {
            var collection = await context.Collection.FindAsync(id);
            var product = await context.Product.FindAsync(idp);

            CollectionProduct cprod = null;

            if (product == null || collection == null) return null;

            foreach (CollectionProduct cp in collection.CollectionProducts)
            {
                if (cp.ProdutctId == idp)
                {
                    cprod = cp;
                }
            }

            if (cprod == null) return null;

            context.CollectionProduct.Remove(cprod);
            await context.SaveChangesAsync();

            return collection;
        }
    }
}