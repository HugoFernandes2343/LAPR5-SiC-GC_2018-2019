using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sic.DTO;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PersistenceContext _context;

        public OrderController(PersistenceContext context)
        {
            _context = context;
        }

        /* Posts an empty order */
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderDTO value)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (value == null) return BadRequest();

                    Order order = new Order();

                    var user = _context.users.Where(u => u.Email == value.Email).FirstOrDefault();

                    order.Date = value.Date;
                    order.UserId = user.Id;

                    _context.orders.Add(order);
                    await _context.SaveChangesAsync();

                    return Ok(order);

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });

        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Entry(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        /* Gets a specific order by id */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] long id)
        {

            if (ModelState.IsValid)
            {
                Order dbOrder;

                try{
                    dbOrder = _context.orders.Where(o => o.Id == id).First();
                }catch(Exception e){
                    return NotFound(e.Message);
                }

                if (dbOrder == null) return NotFound();

                GetOrderDTO value = new GetOrderDTO();

                var user = await _context.users.Where(u => u.Id == dbOrder.UserId).FirstAsync();

                value.Date = dbOrder.Date;
                value.Email = user.Email;
                value.Cost = getCostOfOrder(id);

                return Ok(value);
            }

            return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });

        }

        /* Returns a list of all products of a certain order */
        public double getCostOfOrder(long id)
        {
            List<OrderAndProduct> list = new List<OrderAndProduct>();
            list = _context.ordersAndProducts.Where(o => o.OrderId == id).ToList();
            double totalCost = 0;

            foreach (OrderAndProduct o in list)
            {
                var prod = _context.products.Where(p => p.Id == o.ProductId).FirstOrDefault();
                if (prod != null) totalCost += prod.Price;
            }

            return totalCost;
        }

        // [HttpGet("{orderId}/item/{productIndex}")]
        // public async Task<IActionResult> GetOrderItem([FromRoute]long orderId, [FromRoute]int productIndex)
        // {
        //     List<GetProductDTO> orderProducts = getProductsOfOrder(orderId);
            
        //     if (orderProducts.Count == 0) return NotFound();
            
        //     if (productIndex > orderProducts.Count) return NotFound();

        //     return Ok(orderProducts[productIndex-1]);
        // }

        [HttpGet("{id}/items")]
        public IActionResult getOrderItems([FromRoute] long id)
        {
            return Content(JsonConvert.SerializeObject(new { data = getProductsOfOrder(id) }, Formatting.Indented), "application/json");
        }

        /* function that gets the items of a specific order */
        public List<GetProductDTO> getProductsOfOrder(long id)
        {
            List<OrderAndProduct> list = new List<OrderAndProduct>();
            List<GetProductDTO> orderProducts = new List<GetProductDTO>();
            list = _context.ordersAndProducts.Where(o => o.OrderId == id).ToList();

            foreach (OrderAndProduct o in list)
            {
                var prod = _context.products.Where(p => p.Id == o.ProductId).FirstOrDefault();

                GetProductDTO product = new GetProductDTO();

                product.Name = prod.Name;
                product.Description = prod.Description;
                product.Price = prod.Price;

                orderProducts.Add(product);

                List<Product> childProds = _context.products.Where(p => p.ParentId == prod.Id).ToList();

                foreach (Product child in childProds)
                {
                    GetProductDTO p = new GetProductDTO();
                    p.Name = child.Name;
                    p.Description = child.Description;
                    p.Price = child.Price;

                    orderProducts.Add(p);
                }

            }

            return orderProducts;
        }

        /* function that returns all orders */
        [HttpGet]
        public IEnumerable<Order> getAllOrders()
        {
            return _context.orders.ToList();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] long id, [FromBody] OrderDTO value)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    if (value == null) return BadRequest();

                    var order = await _context.orders.FindAsync(id);

                    if (order == null) return NotFound();

                    order.Date = value.Date;
                    order.UserId = value.UserId;

                    _context.Entry(order).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }

                    return Ok(order);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
        }

    }
}