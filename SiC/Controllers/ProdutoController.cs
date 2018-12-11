using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using SiC.Services;

namespace SiC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService service;

        public ProdutoController(SiCContext context)
        {
            service = new Services.ProdutoService(context);
        }

        // GET: api/Produto
        [HttpGet]
        public IEnumerable<DTOs.ProdutosDTO> GetProdutos()
        {
            return service.getAllProdutos();
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = await service.getProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto([FromRoute] long id, [FromBody] DTOs.ProdutosDTO produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.id)
            {
                return BadRequest();
            }

            var result = await service.editarProduto(produto);

            if (result == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(produto);
            }
        }

        // POST: api/Produto
        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] ProdutosDTO produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.guardarProduto(produto);

            return CreatedAtAction("GetProduto", new { id = produto.id }, produto);
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Produto produto = await service.eliminarProduto(id);

            if (produto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(produto);
            }
        }

        // GET: api/Produto/?nome={nome}
        [HttpGet("nome={name}")]
        public async Task<IActionResult> GetProdutoByName([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produto = await service.getProdutoByName(name);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // GET: api/Produto/{id}/Partes
        [HttpGet("{id}/Partes")]
        public async Task<List<ProdutosDTO>> GetPartes([FromRoute] long id)
        {
            return await service.GetPartes(id);
        }

        // GET: api/Produto/{id}/PartesEm
        [HttpGet("{id}/PartesEm")]
        public async Task<List<ProdutosDTO>> GetProdutoFazParte([FromRoute] long id)
        {
            return await service.GetProdutoFazParte(id);
        }
        
    }
}