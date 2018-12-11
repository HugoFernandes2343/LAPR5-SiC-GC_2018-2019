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
    public class MaterialController : ControllerBase
    {
        private static Services.MaterialService serv;

        public MaterialController(SiCContext context)
        {
            serv = new Services.MaterialService(context);
        }

        // GET: api/Material
        [HttpGet]
        public IEnumerable<Material> GetMaterias()
        {
            return serv.getAllMateriais();
        }

        // GET: api/Material/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var material = await serv.getMaterial(id);

            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }

        // PUT: api/Material/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial([FromRoute] long id, [FromBody] Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != material.Id)
            {
                return BadRequest();
            }

            var result = await serv.editarMaterial(material);
            
            if(result == 0)
            {
                return NoContent();

            }else{

                return NotFound();
            }
        }

        // POST: api/Material
        [HttpPost]
        public async Task<IActionResult> PostMaterial([FromBody] Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serv.guardarMaterial(material);

            return CreatedAtAction("GetMaterial", new { id = material.Id }, material);
        }

        // DELETE: api/Material/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Material material = await serv.eliminarMaterial(id);
            
            if(material == null)
            {
                return NotFound();
            }else{
                return Ok(material);
            }
            
        }

    }
}
    