using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class FinishingRepository : Repository<Finishing, FinishingDTO>
    {

        private SiCContext context;

        public FinishingRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Finishing> Add(FinishingDTO dto)
        {
            if (context.Finishing.Any(f => f.name == dto.name)) return null;

            Finishing finishing = new Finishing();
            finishing.name = dto.name;
            finishing.description = dto.description;
            finishing.Prices = new List<Price>();
            context.Finishing.Add(finishing);
            await context.SaveChangesAsync();

            return finishing;
        }

        public async Task<Finishing> Edit(int id, FinishingDTO dto)
        {
            var finishing = await context.Finishing.FindAsync(id);

            if (finishing == null) return null;

            if (context.Finishing.Any(f => f.name == dto.name && f.FinishingId != id)) return null;

            finishing.name = dto.name;
            finishing.description = dto.description;

            context.Entry(finishing).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return finishing;
        }

        public IEnumerable<Finishing> FindAll()
        {
            return context.Finishing;
        }

        public async Task<Finishing> FindById(int id)
        {
            return await context.Finishing.FindAsync(id);
        }

        public async Task<Finishing> Remove(int id)
        {
            var finishing = await context.Finishing.FindAsync(id);

            if (finishing == null) return null;

            foreach (MaterialFinishing mf in finishing.MaterialFinishings)
            {
                context.MaterialFinishing.Remove(mf);
            }

            foreach (Price pr in finishing.Prices)
            {
                context.Price.Remove(pr);
            }

            context.Finishing.Remove(finishing);
            await context.SaveChangesAsync();

            return finishing;
        }

        internal async Task<Finishing> AddPrice(int id, int idd)
        {
            var finish = await context.Finishing.FindAsync(id);
            var price = await context.Price.FindAsync(idd);

            if (finish == null || price == null) return null;

            if (finish.Prices.Contains(price)) return null;

            finish.Prices.Add(price);

            context.Entry(finish).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return finish;

        }
    }
}