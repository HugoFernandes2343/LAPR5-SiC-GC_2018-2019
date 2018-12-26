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
    [Route("api/Catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogRepository catalogRepository;

        public CatalogController(SiCContext context)
        {
            catalogRepository = new CatalogRepository(context);
        }

        // GET: api/Catalog
        //Returns all the catalogs
        [HttpGet]
        public IEnumerable<CatalogDTO> GetCatalog()
        {
            List<CatalogDTO> dtos = new List<CatalogDTO>();
            foreach (Catalog catalog in catalogRepository.FindAll())
            {
                CatalogDTO dto = new CatalogDTO();
                dto.CatalogId = catalog.CatalogId;
                dto.Date = catalog.Date;
                dto.CatalogDescription = catalog.CatalogDescription;
                dto.CatalogName = catalog.CatalogName;
                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Catalog/id
        //Returns a specific catalog
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.FindById(id);

            if (catalog == null)
            {
                return NotFound();
            }
            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.Date = catalog.Date;
            dto.CatalogDescription = catalog.CatalogDescription;

            foreach (Product prod in catalog.Products)
            {
                ProductDTO prodDTO = new ProductDTO();
                prodDTO.ProductId = prod.ProductId;
                prodDTO.name = prod.name;
                prodDTO.description = prod.description;
                //get Category
                CategoryDTO catDTO = new CategoryDTO();
                catDTO.CategoryId = prod.category.CategoryId;
                catDTO.name = prod.category.name;
                catDTO.description = prod.category.description;

                dto.products.Add(prodDTO);
            }

            return Ok(dto);
        }

        // PUT: api/Catalog/id
        //Updates a catalog with the given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog([FromRoute] int id, [FromBody] CatalogDTO catalogDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalogDTO.CatalogId)
            {
                return BadRequest();
            }

            var catalog = await catalogRepository.FindById(id);

            if (catalog == null)
            {
                return BadRequest();
            }
            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.CatalogDescription = catalog.CatalogDescription;
            dto.Date = catalog.Date;

            return Ok(dto);
        }

        // POST: api/Catalog
        // Creates a catalog
        [HttpPost]
        public async Task<IActionResult> PostCatalog([FromBody] CatalogDTO catalogDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.Add(catalogDTO);

            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.CatalogDescription = catalog.CatalogDescription;
            dto.Date = catalog.Date;

            return CreatedAtAction("GetCatalog", dto);
        }

        // DELETE: api/Catalog/id
        // Deletes a catalog with the given id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.Remove(id);
            if (catalog == null)
            {
                return NotFound();
            }

            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.Date = catalog.Date;
            dto.CatalogDescription = catalog.CatalogDescription;

            return Ok(dto);
        }
    }
}