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

namespace SiC.Controllers
{
    [Route("api/Finishing")]
    [ApiController]
    public class FinishingController : Controller
    {
        private readonly FinishingRepository finishingRepository;

        public FinishingController(SiCContext context)
        {
            finishingRepository = new FinishingRepository(context);
        }

        // GET: api/Finishing
        [HttpGet]
        public IEnumerable<FinishingDTO> GetFinishing()
        {
            List<FinishingDTO> dtos = new List<FinishingDTO>();
            foreach (Finishing finishing in finishingRepository.FindAll()){
                FinishingDTO dto = new FinishingDTO();
                dto.finishingId = finishing.FinishingId;
                dto.description = finishing.description;
                dto.name = finishing.name;
                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/Finishing/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFinishing([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finishing = await finishingRepository.FindById(id);

            if (finishing == null)
            {
                return NotFound();
            }

            FinishingDTO dto = new FinishingDTO();
            dto.finishingId = finishing.FinishingId;
            dto.description = finishing.description;
            dto.name = finishing.name;

            return Ok(dto);
        }

        // PUT: api/Finishing/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinishing([FromRoute] int id, [FromBody] FinishingDTO finishingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != finishingDTO.finishingId)
            {
                return BadRequest();
            }

            var finishing = await finishingRepository.Edit(id, finishingDTO);

            if(finishing == null){
                return BadRequest();
            }

            FinishingDTO dto = new FinishingDTO();
            dto.finishingId = finishing.FinishingId;
            dto.description = finishing.description;
            dto.name = finishing.name;

            return Ok(dto);
        }

        // POST: api/Finishing
        [HttpPost]
        public async Task<IActionResult> PostFinishing([FromBody] FinishingDTO finishingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finishing = await finishingRepository.Add(finishingDTO);

            if(finishing == null){
                return BadRequest();
            }

            FinishingDTO dto = new FinishingDTO();
            dto.finishingId = finishing.FinishingId;
            dto.description = finishing.description;
            dto.name = finishing.name;

            return CreatedAtAction("GetFinishing", dto);
        }

        // DELETE: api/Finishing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinishing([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finishing = await finishingRepository.Remove(id);

            if(finishing == null){
                return BadRequest();
            }

            FinishingDTO dto = new FinishingDTO();
            dto.finishingId = finishing.FinishingId;
            dto.description = finishing.description;
            dto.name = finishing.name;

            return Ok(dto);
        }
    }
}