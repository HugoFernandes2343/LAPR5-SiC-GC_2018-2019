using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using SiC.Repository;

namespace SiC.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository orderRepository;

        public OrderController(SiCContext context)
        {
            orderRepository = new OrderRepository(context);
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<OrderDTO> GetOrder()
        {
            List<OrderDTO> dtos = new List<OrderDTO>();
            foreach (Order order in orderRepository.FindAll())
            {
                OrderDTO dto = new OrderDTO();
                dto.OrderId = order.OrderId;
                dto.OrderName = order.OrderName;
                dto.address = order.address;
                dto.date = order.date;
                dto.cost = order.cost;
                dto.status = order.status;
                dto.orderItems = new List<ProductDTO>();

                foreach (Product product in order.orderItems)
                {
                    ProductDTO prod_dto = new ProductDTO();
                    prod_dto.ProductId = product.ProductId;
                    prod_dto.name = product.name;
                    prod_dto.description = product.description;
                    //get Category
                    CategoryDTO cat_dto = new CategoryDTO();
                    cat_dto.CategoryId = product.category.CategoryId;
                    cat_dto.name = product.category.name;
                    cat_dto.description = product.category.description;

                    dto.orderItems.Add(prod_dto);
                }

                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/Order/id
        //Returns a specific order
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await orderRepository.FindById(id);

            if (order == null)
            {
                return NotFound();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }

            return Ok(order_dto);
        }


        // PUT: api/Product/id
        //Updates an order with the given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDTO.OrderId)
            {
                return BadRequest();
            }

            var order = await orderRepository.Edit(id, orderDTO);

            if (order == null)
            {
                return BadRequest();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }

            return Ok(order_dto);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await orderRepository.Add(orderDTO);

            if (order == null)
            {
                return BadRequest();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }

            return CreatedAtAction("GetOrder", order_dto);
        }

        // DELETE: api/Order/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await orderRepository.Remove(id);

            if (order == null)
            {
                return BadRequest();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }

            return Ok(order_dto);
        }

        // GET: api/Order/Search/{name}
        // Search an order with user name
        [HttpGet("Search/{name}")]
        public async Task<IActionResult> GetOrderByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await orderRepository.FindByName(name);

            if (order == null)
            {
                return NotFound();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }
            
            return Ok(order_dto);
        }

        // PUT: api/Order/id/OrderItems/idd
        [HttpPut("{id}/OrderItems/{idd}")]
        public async Task<IActionResult> PutOrderItem([FromRoute] int id, [FromRoute] int idd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await orderRepository.AddItem(id, idd);

            if (order == null)
            {
                return BadRequest();
            }

            OrderDTO order_dto = new OrderDTO();
            order_dto.OrderId = order.OrderId;
            order_dto.OrderName = order.OrderName;
            order_dto.address = order.address;
            order_dto.date = order.date;
            order_dto.cost = order.cost;
            order_dto.status = order.status;
            order_dto.orderItems = new List<ProductDTO>();

            
            foreach (Product prod in order.orderItems)
            {
                ProductDTO prod_dto = new ProductDTO();
                prod_dto.ProductId = prod.ProductId;
                prod_dto.name = prod.name;
                prod_dto.description = prod.description;
                //get Category
                CategoryDTO cat_dto = new CategoryDTO();
                cat_dto.CategoryId = prod.category.CategoryId;
                cat_dto.name = prod.category.name;
                cat_dto.description = prod.category.description;

                order_dto.orderItems.Add(prod_dto);
            }
            
            return Ok(order_dto);
        }
    }
}