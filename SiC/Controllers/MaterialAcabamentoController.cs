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
    public class MaterialAcabamentoController : ControllerBase
    {
        private static Services.MaterialAcabamentoService serv;
        private static Services.MaterialService servMat;
        private static Services.AcabamentoService servAca;

        public MaterialAcabamentoController(SiCContext context)
        {
            serv = new Services.MaterialAcabamentoService(context);
            servMat = new Services.MaterialService(context);
            servAca = new Services.AcabamentoService(context);
        }

        // GET: api/MaterialAcabamento
        [HttpGet]
        public IEnumerable<DTOs.MaterialAcabamentoDTO> GetMaterialAcabamentos()
        {
            return serv.getAllMateriaisAcabamentos();
        }

        // GET: api/MaterialAcabamento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialAcabamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            DTOs.MaterialAcabamentoDTO materialAcabamento = await serv.getMaterialAcabamento(id);

            if (materialAcabamento == null)
            {
                return NotFound();
            }

            return Ok(materialAcabamento);
        }

        // PUT: api/MaterialAcabamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialAcabamento([FromRoute] long id, [FromBody] DTOs.MaterialAcabamentoDTO materialAcabamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialAcabamento.Id)
            {
                return BadRequest();
            }

            var result = await serv.editarMaterialAcabamento(materialAcabamento);

            if (result == 0)
            {
                return NoContent();

            }
            else
            {

                return NotFound();
            }
        }

        // POST: api/MaterialAcabamento
        [HttpPost]
        public async Task<IActionResult> PostMaterialAcabamento([FromBody] DTOs.MaterialAcabamentoDTO materialAcabamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serv.guardarMaterialAcabamento(materialAcabamento);

            return CreatedAtAction("GetMaterialAcabamento", new { id = materialAcabamento.Id }, materialAcabamento);
        }

        // DELETE: api/MaterialAcabamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialAcabamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DTOs.MaterialAcabamentoDTO materialAcabamento = await serv.eliminarMaterialAcabamento(id);

            if (materialAcabamento == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(materialAcabamento);
            }

        }

    }
}
