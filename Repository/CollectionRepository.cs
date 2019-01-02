using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using System;
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
            collection.products = new List<Product>(); /// TODO check with hugo if this is correct
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
            if(collection == null) return null;

            context.Collection.Remove(collection);
            await context.SaveChangesAsync();

            return collection;
        }
    }
}