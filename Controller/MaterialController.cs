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
    public class MaterialController : ControllerBase
    {
        private readonly PersistenceContext _context;
        private MaterialRepository materialRepository;

        public MaterialController(PersistenceContext context)
        {
            _context = context;
            materialRepository = new MaterialRepository(context);
        }

        [HttpGet]
        public IEnumerable<Material> GetAllMaterials()
        {
            List<Material> materials = new List<Material>();

            foreach (Material m in materialRepository.FindAll().ToList())
            {
                List<MaterialFinish> finishes = _context.materialFinishes.Where(f => f.MaterialId == m.Id).ToList();
                foreach (MaterialFinish f in finishes)
                {
                    var finish = _context.finishes.Find(f.FinishId);
                    m.Finishes.Add(finish);
                }
                materials.Add(m);
            }
            return materials;
        }

        [HttpGet, Route("~/api/materialfinish/{id}")]
        public async Task<IActionResult> GetMaterialById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var material = await materialRepository.FindById(id);

            if (material == null) return NotFound();

            List<MaterialFinish> finishes = _context.materialFinishes.Where(f => f.MaterialId == material.Id).ToList();
            foreach (MaterialFinish f in finishes)
            {
                var finish = _context.finishes.Find(f.FinishId);
                material.Finishes.Add(finish);
            }

            return Ok(material);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial([FromRoute] long id, [FromBody] MaterialDTO value)
        {
            if (ModelState.IsValid)
            {

                if (value == null) return BadRequest();

                var material = await materialRepository.Edit(id, value);

                if (material == null) return NotFound();

                return Ok(material);

            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }

        [HttpDelete("~/api/material/productmaterial")]
        public IActionResult DeleteProductMaterial([FromBody] EditMaterialDTO value)
        {
            if (ModelState.IsValid)
            {

                if (value == null) return BadRequest();

                var material = materialRepository.RemoveProductMaterial(value);

                if (material == null) return NotFound();

                return Ok(material);

            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }
        [HttpPost("~/api/material/productmaterial")]
        public IActionResult AddProductMaterial([FromBody] EditMaterialDTO value)
        {
            if (ModelState.IsValid)
            {

                if (value == null) return BadRequest();

                var material = materialRepository.CreateProductMaterial(value);

                if (material == null) return NotFound();

                return Ok(material);

            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }

        // POST: api/Material
        [HttpPost]
        public async Task<IActionResult> PostMaterial([FromBody] MaterialDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (value == null) return BadRequest();

            var material = await materialRepository.Add(value);

            if (material == null)
                return BadRequest();

            return Ok(material);

        }

        // DELETE: api/Material/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var material = await materialRepository.FindById(id);

            if (material == null) return NotFound();

            return Ok(material);
        }

        private bool MaterialExists(long id)
        {
            return _context.materials.Any(e => e.Id == id);
        }
    }
}