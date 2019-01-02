using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class OrderRepository : Repository<Order, OrderDTO>
    {
        private SiCContext context;

        public OrderRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Order> Add(OrderDTO dto)
        {
            if (context.Order.Any(o => o.OrderId == dto.OrderId)) return null;

            List<Product> items = new List<Product>();

            Order order = new Order();
            order.OrderId = dto.OrderId;
            order.OrderName = dto.OrderName;
            order.date = dto.date;
            order.address = dto.address;
            order.cost = order.cost;
            order.status = order.status;
            order.orderItems = items;

            context.Order.Add(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> Edit(int id, OrderDTO dto)
        {

            var order = await context.Order.FindAsync(id);

            if(order == null) return null;

            if(context.Order.Any(o => o.OrderName == dto.OrderName && o.OrderId != id)) return null;

            order.OrderName = dto.OrderName;
            order.address = dto.address;
            order.date = dto.date;
            order.cost = dto.cost;
            order.status = dto.status;

            context.Entry(order).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return order;
        }

        public IEnumerable<Order> FindAll()
        {
            return context.Order;
        }

        public async Task<Order> FindById(int id)
        {
            return await context.Order.FindAsync(id);
        }

        public async Task<Order> Remove(int id)
        {
            var order = await context.Order.FindAsync(id);

            if (order == null) return null;

            foreach (Product dim in order.orderItems)
            {
                context.Product.Remove(dim);
            }

            context.Order.Remove(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> FindByName(string name)
        {
            return await context.Order.SingleOrDefaultAsync(o => o.OrderName == name);
        }

        internal async Task<Order> AddItem(int id, int idd)
        {
            var order = await context.Order.FindAsync(id);
            var item = await context.Product.FindAsync(idd);

            if (order == null || item == null) return null;

            if (order.orderItems.Contains(item)) return null;

            order.orderItems.Add(item);

            context.Entry(order).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return order;

        }
    }
}