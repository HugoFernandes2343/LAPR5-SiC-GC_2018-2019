using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.Models;

namespace SiC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcabamentoController : ControllerBase
    {
        private static Services.AcabamentoService serv;

        public AcabamentoController(SiCContext context)
        {
            serv = new Services.AcabamentoService(context);
        }

        // GET: api/Acabamento
        [HttpGet]
        public IEnumerable<Acabamento> GetAcabamentos()
        {
            return serv.getAllAcabamentos();
        }

        // GET: api/Acabamento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcabamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var acabamento = await serv.getAcabamento(id);

            if (acabamento == null)
            {
                return NotFound();
            }

            return Ok(acabamento);
        }

        // PUT: api/Acabamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcabamento([FromRoute] long id, [FromBody] Acabamento acabamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != acabamento.Id)
            {
                return BadRequest();
            }

            var result = await serv.editarAcabamento(acabamento);
            
            if(result == 0)
            {
                return NoContent();

            }else{

                return NotFound();
            }
        }

        // POST: api/Acabamento
        [HttpPost]
        public async Task<IActionResult> PostAcabamento([FromBody] Acabamento acabamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serv.guardarAcabamento(acabamento);

            return CreatedAtAction("GetAcabamento", new { id = acabamento.Id }, acabamento);
        }

        // DELETE: api/Acabamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcabamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Acabamento acabamento = await serv.eliminarAcabamento(id);
            
            if(acabamento == null)
            {
                return NotFound();
            }else{
                return Ok(acabamento);
            }
            
        }

    }
}
    