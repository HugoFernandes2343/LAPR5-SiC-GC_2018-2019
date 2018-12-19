using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogRepository catalogRepository;

        public CatalogController(PersistenceContext context)
        {
            catalogRepository = new CatalogRepository(context);
        }

        [HttpGet]
        public IEnumerable<Catalog> GetAll()
        {
            return catalogRepository.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var catalog = await catalogRepository.FindById(id);

            if (catalog == null) return NotFound();

            return Ok(catalog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog([FromRoute] long id, [FromBody]CatalogDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value == null) return BadRequest();

            var catalog = await catalogRepository.Edit(id, value);

            if (catalog == null) return NotFound();

            return Ok(catalog);
        }

        [HttpPost]
        public async Task<IActionResult> PostCatalog([FromBody]CatalogDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (value == null || value.Date == null) return BadRequest();

            var catalog = await catalogRepository.Add(value);

            return Ok(catalog);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var catalog = await catalogRepository.Remove(id);
            if (catalog == null) return NotFound();

            return Ok(catalog);
        }
    }
}