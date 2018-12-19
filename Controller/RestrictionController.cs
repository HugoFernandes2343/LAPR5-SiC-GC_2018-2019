using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.Model;
using SiC.Persistence;
using SiC.DTO;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionController : ControllerBase
    {
        private readonly RestrictionRepository restrictionRepository;

        public RestrictionController(PersistenceContext context)
        {
            this.restrictionRepository = new RestrictionRepository(context);
        }

        // GET: api/Restriction
        [HttpGet]
        public IEnumerable<Restriction> Getrestrictions()
        {
            return restrictionRepository.FindAll();
        }

        // GET: api/Restriction/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestrictionById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var restriction = await restrictionRepository.FindById(id);

            if (restriction == null) return NotFound();

            return Ok(restriction);
        }

        // PUT: api/Restriction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestriction([FromRoute] long id, [FromBody] RestrictionDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (value == null) return BadRequest();

            var restriction = await restrictionRepository.Edit(id, value);

            if (restriction == null) return NotFound();

            return Ok();
        }

        // POST: api/Restriction
        [HttpPost]
        public async Task<IActionResult> PostRestriction([FromBody] RestrictionDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (value == null) return BadRequest();

            var restriction = await restrictionRepository.Add(value);

            return Ok(restriction);
        }

        // DELETE: api/Restriction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestriction([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restriction = await restrictionRepository.Remove(id);

            if (restriction == null) return NotFound();

            return Ok(restriction);
        }
    }
}