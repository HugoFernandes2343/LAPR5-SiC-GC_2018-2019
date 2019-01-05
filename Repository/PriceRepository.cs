using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class PriceRepository : Repository<Price, PriceDTO>
    {
        private SiCContext context;

        public PriceRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Price> Add(PriceDTO dto)
        {
            if(dto.price < 0.0){
                return null;
            }
            string designation = dto.designation;
            double price_value = dto.price;
            DateTime date = DateTime.Parse(dto.date);

            Price price = new Price();
            price.designation = designation;
            price.price= price_value;
            price.date = date;

            context.Price.Add(price);
            await context.SaveChangesAsync();

            return price;
        }

        public async Task<Price> Edit(int id, PriceDTO dto)
        {

            var price = await context.Price.FindAsync(id);

            if (price == null) return null;

            //if (context.Price.Any(p => p.name == dto.name && p.ProductId != id)) return null;

            price.designation = dto.designation;
            price.price = dto.price;
            price.date = DateTime.Parse(dto.date);

            context.Entry(price).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return price;
        }

        public async Task<List<Price>> FindByEntity(string name)
        {
            List<Price> prices = new List<Price>();

            var query = from p in context.Price where p.designation == name select p;
            prices = query.ToList();

            return prices; 
        }

        public IEnumerable<Price> FindAll()
        {
            return context.Price;
        }

        public async Task<Price> FindById(int id)
        {
            return await context.Price.FindAsync(id);
        }

        public async Task<Price> Remove(int id)
        {
            var price = await context.Price.FindAsync(id);

            if (price == null) return null;

            context.Price.Remove(price);
            await context.SaveChangesAsync();

            return price;
        }

    }
}