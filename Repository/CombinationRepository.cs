using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class CombinationRepository : Repository<Combination, CombinationDTO>
    {

        private SiCContext context;

        public CombinationRepository(SiCContext context) 
        {
            this.context = context;
        }

        public async Task<Combination> Add(CombinationDTO dto)
        {
            var containingProduct = await context.Product.SingleOrDefaultAsync(p => p.name == dto.containingProduct);
            var containedProduct = await context.Product.SingleOrDefaultAsync(p => p.name == dto.containedProduct);

            if (containedProduct == null || containingProduct == null)
            {
                return null;
            }

            if (context.Combination.Any(c => c.containedProduct.name == dto.containedProduct && c.containingProduct.name == dto.containingProduct))
            {
                return null;
            }

            Combination combination;
            try
            {
                combination = new Combination(containingProduct, containedProduct, dto.required);
            }
            catch (ArgumentException)
            {
                return null;
            }

            context.Combination.Add(combination);
            await context.SaveChangesAsync();
            return combination;
        }

        public async Task<Combination> Edit(int id, CombinationDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Combination> FindAll()
        {
            return context.Combination;
        }

        public async Task<Combination> FindById(int id)
        {
            return await context.Combination.FindAsync(id);
        }

        public async Task<Combination> Remove(int id)
        {
            var combination = await context.Combination.FindAsync(id);
            if (combination == null)
            {
                return null;
            }

            context.Combination.Remove(combination);
            await context.SaveChangesAsync();
            return combination;
        }
    }
}