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
    public class DimensionController : ControllerBase
    {
        private readonly PersistenceContext _context;
        private DimensionRepository dimensionRepository;

        public DimensionController(PersistenceContext context)
        {
            _context = context;
            dimensionRepository = new DimensionRepository(context);
        }

        [HttpGet]
        public IEnumerable<Dimension> GetAll()
        {
            return dimensionRepository.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDimensionById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Dimension dimension = await dimensionRepository.FindById(id);

            if(dimension == null) return NotFound();

            return Ok(dimension);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimension([FromRoute] long id, [FromBody]DimensionDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(value == null) return BadRequest(ModelState);
            
            var dimension = await dimensionRepository.Edit(id,value);

            if(dimension == null) return NotFound();

            return Ok(dimension);
        }

        [HttpPost]
        public async Task<IActionResult> PostDimension([FromBody]DimensionDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value == null) return BadRequest();

            var dimension = await dimensionRepository.Add(value);

            if(dimension == null) return BadRequest();

            return Ok(dimension);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimension([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dimension = await _context.dimensions.FindAsync(id);
            
            if (dimension == null)
            {
                return NotFound();
            }

            _context.dimensions.Remove(dimension);
            await _context.SaveChangesAsync();

            return Ok(dimension);
        }

        private bool DimensionExists(long id)
        {
            return _context.dimensions.Any(e => e.Id == id);
        }
    }
}