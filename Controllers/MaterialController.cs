using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using SiC.Repository;

namespace SIC.Controllers
{
    [Route("api/Material")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialRepository materialRepository;

        public MaterialController(SiCContext context)
        {
            materialRepository = new MaterialRepository(context);
        }


        // GET: api/Product
        [HttpGet]
        public IEnumerable<MaterialDTO> GetProduct()
        {
            List<MaterialDTO> dtos = new List<MaterialDTO>();

            foreach (Material material in materialRepository.FindAll())
            {
                MaterialDTO dto = new MaterialDTO();
                dto.name = material.name;
                dto.description = material.description;
                dto.MaterialId = material.MaterialId;
                dto.finishes = new List<FinishingDTO>();
                dto.prices =  new List<PriceDTO>();

                foreach (MaterialFinishing mf in material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    fdto.prices = new List<PriceDTO>();

                    foreach (Price pr in mf.Finishing.Prices){
                        PriceDTO prdto = new PriceDTO();
                        prdto.designation = prdto.designation;
                        prdto.price = prdto.price;
                        prdto.date = prdto.date;
                        fdto.prices.Add(prdto);
                    }

                    dto.finishes.Add(fdto);
                }

                foreach (Price pr in material.Prices){
                    PriceDTO prdto = new PriceDTO();
                    prdto.designation = prdto.designation;
                    prdto.price = prdto.price;
                    prdto.date = prdto.date;
                    dto.prices.Add(prdto);
                }


                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Material/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await materialRepository.FindById(id);

            if (material == null)
            {
                return NotFound();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();
            dto.prices =  new List<PriceDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                    foreach (Price pr in mf.Finishing.Prices){
                        PriceDTO prdto = new PriceDTO();
                        prdto.designation = prdto.designation;
                        prdto.price = prdto.price;
                        prdto.date = prdto.date;
                        fdto.prices.Add(prdto);
                    }

                dto.finishes.Add(fdto);
            }

            foreach (Price pr in material.Prices){
                PriceDTO prdto = new PriceDTO();
                prdto.designation = prdto.designation;
                prdto.price = prdto.price;
                prdto.date = prdto.date;
                dto.prices.Add(prdto);
            }


            return Ok(dto);
        }

        // PUT: api/Material/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial([FromRoute] int id, [FromBody] MaterialDTO materialDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialDTO.MaterialId)
            {
                return BadRequest();
            }

            var material = await materialRepository.Edit(id, materialDTO);

            if (material == null)
            {
                return BadRequest();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                    foreach (Price pr in mf.Finishing.Prices){
                        PriceDTO prdto = new PriceDTO();
                        prdto.designation = prdto.designation;
                        prdto.price = prdto.price;
                        prdto.date = prdto.date;
                        fdto.prices.Add(prdto);
                    }

                dto.finishes.Add(fdto);
            }

            foreach (Price pr in material.Prices){
                    PriceDTO prdto = new PriceDTO();
                    prdto.designation = prdto.designation;
                    prdto.price = prdto.price;
                    prdto.date = prdto.date;
                    dto.prices.Add(prdto);
            }

            return Ok(dto);
        }

        // POST: api/Material
        [HttpPost]
        public async Task<IActionResult> PostMaterial([FromBody] MaterialDTO materialDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await materialRepository.Add(materialDTO);

            if (material == null)
            {
                return BadRequest();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();
            dto.prices = new List<PriceDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                    foreach (Price pr in mf.Finishing.Prices){
                        PriceDTO prdto = new PriceDTO();
                        prdto.designation = prdto.designation;
                        prdto.price = prdto.price;
                        prdto.date = prdto.date;
                        fdto.prices.Add(prdto);
                    }

                dto.finishes.Add(fdto);
            }

            foreach (Price pr in material.Prices){
                PriceDTO prdto = new PriceDTO();
                prdto.designation = prdto.designation;
                prdto.price = prdto.price;
                prdto.date = prdto.date;
                dto.prices.Add(prdto);
            }

            return CreatedAtAction("GetMaterial", dto);
        }

        // DELETE: api/Material/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await materialRepository.Remove(id);

            if (material == null)
            {
                return BadRequest();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();
            dto.prices = new List<PriceDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                foreach (Price pr in mf.Finishing.Prices){
                    PriceDTO prdto = new PriceDTO();
                    prdto.designation = prdto.designation;
                    prdto.price = prdto.price;
                    prdto.date = prdto.date;
                    fdto.prices.Add(prdto);
                }

                dto.finishes.Add(fdto);
            }

            foreach (Price pr in material.Prices){
                PriceDTO prdto = new PriceDTO();
                prdto.designation = prdto.designation;
                prdto.price = prdto.price;
                prdto.date = prdto.date;
                dto.prices.Add(prdto);
            }

            return Ok(dto);
        }

        // PUT: api/Material/id/Finishing/idf
        [HttpPut("{id}/Finishing/{idf}")]
        public async Task<IActionResult> PutMaterialFinishing([FromRoute] int id, [FromRoute] int idf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await materialRepository.AddMaterialFinishing(id, idf);

            if (material == null)
            {
                return BadRequest();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();
            dto.prices = new List<PriceDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                foreach (Price pr in mf.Finishing.Prices){
                    PriceDTO prdto = new PriceDTO();
                    prdto.designation = prdto.designation;
                    prdto.price = prdto.price;
                    prdto.date = prdto.date;
                    fdto.prices.Add(prdto);
                }

                dto.finishes.Add(fdto);
            }

            foreach (Price pr in material.Prices){
                PriceDTO prdto = new PriceDTO();
                prdto.designation = prdto.designation;
                prdto.price = prdto.price;
                prdto.date = prdto.date;
                dto.prices.Add(prdto);
            }

            return Ok(dto);
        }

        // DELETE: api/Material/id/Finishing/idf
        [HttpDelete("{id}/Finishing/{idf}")]
        public async Task<IActionResult> DeleteMaterialFinishing([FromRoute] int id, [FromRoute] int idf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await materialRepository.RemoveMaterialFinishing(id, idf);

            if (material == null)
            {
                return BadRequest();
            }

            MaterialDTO dto = new MaterialDTO();
            dto.name = material.name;
            dto.description = material.description;
            dto.MaterialId = material.MaterialId;
            dto.finishes = new List<FinishingDTO>();

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                FinishingDTO fdto = new FinishingDTO();
                fdto.finishingId = mf.Finishing.FinishingId;
                fdto.description = mf.Finishing.description;
                fdto.name = mf.Finishing.name;
                fdto.prices = new List<PriceDTO>();

                foreach (Price pr in mf.Finishing.Prices){
                    PriceDTO prdto = new PriceDTO();
                    prdto.designation = prdto.designation;
                    prdto.price = prdto.price;
                    prdto.date = prdto.date;
                    fdto.prices.Add(prdto);
                }
                dto.finishes.Add(fdto);
            }

            return Ok(dto);
        }
    }
}