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
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationController : ControllerBase
    {
        private readonly CombinationRepository combinationRepository;

        public CombinationController(SiCContext context)
        {
            combinationRepository = new CombinationRepository(context);
        }

        // GET: api/Combination
        [HttpGet]
        public IEnumerable<CombinationDTO> GetCombination()
        {
            List<CombinationDTO> combinationDTOs=new List<CombinationDTO>();
            foreach(Combination combination in combinationRepository.FindAll()){
                CombinationDTO dto = new CombinationDTO();
                dto.CombinationId=combination.CombinationId;
                dto.containedProduct=combination.containedProduct.name;
                dto.containingProduct=combination.containingProduct.name;
                dto.required=combination.required;
                combinationDTOs.Add(dto);
            }
            return combinationDTOs;
        }

        // GET: api/Combination/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombination([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var combination = await combinationRepository.FindById(id);

            if (combination == null)
            {
                return NotFound();
            }

            CombinationDTO dto = new CombinationDTO();
            dto.CombinationId=combination.CombinationId;
            dto.containedProduct=combination.containedProduct.name;
            dto.containingProduct=combination.containingProduct.name;
            dto.required=combination.required;

            return Ok(dto);
        }


        // POST: api/Combination
        [HttpPost]
        public async Task<IActionResult> PostCombination([FromBody] CombinationDTO combinationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var combination = await combinationRepository.Add(combinationDTO);

            if(combination == null){
                return BadRequest();
            }

            CombinationDTO dto = new CombinationDTO();
            dto.CombinationId=combination.CombinationId;
            dto.containedProduct=combination.containedProduct.name;
            dto.containingProduct=combination.containingProduct.name;
            dto.required=combination.required;

            return CreatedAtAction("GetCombination", dto);
        }

        // DELETE: api/Combination/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombination([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var combination = await combinationRepository.Remove(id);
            if (combination == null)
            {
                return BadRequest();
            }

            CombinationDTO dto = new CombinationDTO();
            dto.CombinationId=combination.CombinationId;
            dto.containedProduct=combination.containedProduct.name;
            dto.containingProduct=combination.containingProduct.name;
            dto.required=combination.required;

            return Ok(dto);
        }
    }
}