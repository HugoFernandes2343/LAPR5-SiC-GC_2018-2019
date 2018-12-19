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
    public class FinishController : ControllerBase
    {
        private readonly PersistenceContext _context;
        private FinishRepository finishRepository;

        public FinishController(PersistenceContext context)
        {
            _context = context;
            finishRepository = new FinishRepository(context);
        }

        [HttpGet]
        public IEnumerable<Finish> GetAll()
        {
            return finishRepository.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFinishById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var finish = await finishRepository.FindById(id);

            if (finish == null) return NotFound();

            return Ok(finish);
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinish([FromRoute] long id, [FromBody]FinishDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value == null) return BadRequest();

            try
            {
                var finish = await finishRepository.Edit(id,value);

                if (finish == null) return NotFound();

                return Ok(finish);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }

        }



        // POST: api/Finish
        [HttpPost]
        public async Task<IActionResult> PostFinish([FromBody] FinishDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value == null) return BadRequest();
            
            var finish = await finishRepository.Add(value);

            if(finish == null) return BadRequest();
            
            return Ok(finish);
        }

        // DELETE: api/Finish/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinish([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var finish = await finishRepository.Remove(id);

            if (finish == null) return NotFound();

            return Ok(finish);
        }

        private bool FinishExists(long id)
        {
            return _context.finishes.Any(e => e.Id == id);
        }
    }
}