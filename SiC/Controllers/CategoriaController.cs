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
    public class CategoriaController : ControllerBase
    {

        private static Services.CategoriaService serv;

        public CategoriaController(SiCContext context)
        {
            serv = new Services.CategoriaService(context);
        }

        // GET: api/Categoria
        [HttpGet]
        public IEnumerable<DTOs.CategoriaDTO> GetCategorias()
        {
            return serv.getAllCategorias();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var categoria = await serv.getCategoria(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] long id, [FromBody] DTOs.CategoriaDTO categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             if (id != categoria.Id)
            {
                return BadRequest();
            }

            var result = await serv.editarCategoria(categoria);

            if (result == 0)
            {
                return NoContent();

            }
            else
            {

                return Ok(categoria);
            }
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] DTOs.CategoriaDTO categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serv.guardarCategoria(categoria);

            return CreatedAtAction("GetCategoria",  new { id = categoria.Id }, categoria);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DTOs.CategoriaDTO categoria = await serv.eliminarCategoria(id);

            if (categoria == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoria);
            }

        }

    }
}