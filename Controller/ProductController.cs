using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository productRepository;
        private PersistenceContext context;
        public ProductController(PersistenceContext context)
        {
            this.productRepository = new ProductRepository(context);
            this.context = context;
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] long id, [FromBody]PostProductDTO value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (value == null) return BadRequest();
                    
                    var product = await productRepository.Edit(id, value);

                    if (product == null) return BadRequest();

                    return Ok(product);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }

        [HttpPut("addComponent/{parentId}/{childId}")]
        public async Task<IActionResult> AddComponent([FromRoute] long parentId, long childId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await productRepository.AddComponent(parentId, childId);

                    if (product == null) return BadRequest("Could not add product: Either the product doesn't fit or the Ids provided are incorrect.");

                    return Ok(product);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }


        /**
        Adds a product to the database through a post method
        Uses DTO to avoid sending domain objects through Json.
         */
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]PostProductDTO value)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (value == null) return BadRequest();

                    var product = await productRepository.Add(value);

                    if (product == null) return BadRequest();

                    return Ok(product);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }


        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.Remove(id);

            if (product == null) return NotFound();

            return Ok(product);
        }


        //todo ver uma cena nos testes com isto
        // GET /api/product/designation
        [HttpGet("name={name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            Product dbProd;
            try
            {
                dbProd = await productRepository.FindProductByName(name);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            List<Product> components = dbProd.Components.ToList();
            dbProd.Components = components;

            List<Material> materials = dbProd.Materials.ToList();
            dbProd.Materials = materials;

            if (dbProd == null) return NotFound();

            return Ok(dbProd);
        }

        /**
        Returns all the products in the database
        e.g api/product/
         */
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> temp = await productRepository.FindAllAsync();

            if (temp.Count == 0) return NotFound();

            return Ok(temp);
        }


        /*
        Returns the components that are part of a product with {id}.
        e.g api/product/3/components
         */
        [HttpGet("{id}/components")]
        public async Task<IActionResult> GetProductComponents([FromRoute] long Id)
        {
            Product dbProd;

            try
            {
                dbProd = await productRepository.FindById(Id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            List<Product> components = dbProd.Components.ToList();
            dbProd.Components = components;

            if (dbProd.Components.Count == 0) return NoContent();

            return Ok(dbProd.Components);
        }



        /*
        Returns the components that are part of a product with {id}.
        e.g api/product/3/restrictions
         */
        [HttpGet("{id}/restrictions")]
        public async Task<IActionResult> GetProductRestrictions([FromRoute] long Id)
        {
            Product dbProd;

            try
            {
                dbProd = await productRepository.FindById(Id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            List<Product> components = dbProd.Components.ToList();
            dbProd.Components = components;

            if (dbProd.Restriction == null) return NoContent();

            return Ok(dbProd.Restriction);
        }


        /*
        Returns the product with {id} and all its components
        e.g api/product/3/
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]long id)
        {
            if (ModelState.IsValid)
            {

                Product dbProd;

                try
                {
                    dbProd = await productRepository.FindById(id);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

                if (dbProd == null) return NotFound();

                return Ok(dbProd);
            }

            return BadRequest();

        }


        /* method that gets the parent product
        it return error 404 if there is no parent product */
        [HttpGet("{id}/componentin")]
        public async Task<IActionResult> getParentProductLocal([FromRoute] long Id)
        {
            Product childProd;
            try
            {
                childProd = await productRepository.FindChildProduct(Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "child prod");
            }

            if (childProd.ParentId != null)
            {
                Product parentProd;

                try
                {
                    parentProd = await productRepository.FindParentProduct(childProd);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message + "parent prod");
                }

                return Ok(parentProd);
            }

            return NotFound();
        }

    }
}