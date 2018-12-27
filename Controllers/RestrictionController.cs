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
    [Route("api/Restriction")]
    [ApiController]
    public class RestrictionController : ControllerBase
    {
        private readonly RestrictionRepository restrictionRepository;

        public RestrictionController(SiCContext context)
        {
            restrictionRepository = new RestrictionRepository(context);
        }

        // GET: api/Restriction
        [HttpGet]
        public IEnumerable<RestrictionDTO> GetRestriction()
        {
            List<RestrictionDTO> dtos = new List<RestrictionDTO>();
            foreach (Restriction restriction in restrictionRepository.FindAll())
            {
                CombinationDTO combination = new CombinationDTO();
                combination.CombinationId = restriction.combination.CombinationId;
                combination.containedProduct = restriction.combination.containedProduct.name;
                combination.containingProduct = restriction.combination.containingProduct.name;
                combination.required = restriction.combination.required;

                if (restriction is RestrictionDim)
                {
                    RestrictionDim res_dim = (RestrictionDim)restriction;
                    RestrictionDimDTO dto = new RestrictionDimDTO();
                    dto.combination = combination;
                    dto.description = res_dim.description;
                    dto.RestrictionId = res_dim.RestrictionId;
                    dto.x = res_dim.x;
                    dto.y = res_dim.y;
                    dto.z = res_dim.z;
                    dtos.Add(dto);
                }

                if (restriction is RestrictionMat)
                {
                    RestrictionMat res_mat = (RestrictionMat)restriction;
                    RestrictionMatDTO dto = new RestrictionMatDTO();
                    dto.combination = combination;
                    dto.description = res_mat.description;
                    dto.RestrictionId = res_mat.RestrictionId;
                    dto.containingMaterial = res_mat.containingMaterial.name;
                    dto.containedMaterial = res_mat.containedMaterial.name;
                    dtos.Add(dto);
                }

            }
            return dtos;
        }

        // GET: api/Restriction/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestriction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restriction = await restrictionRepository.FindById(id);

            if (restriction == null)
            {
                return NotFound();
            }

            CombinationDTO combination = new CombinationDTO();
            combination.CombinationId = restriction.combination.CombinationId;
            combination.containedProduct = restriction.combination.containedProduct.name;
            combination.containingProduct = restriction.combination.containingProduct.name;
            combination.required = restriction.combination.required;

            if (restriction is RestrictionDim)
            {
                RestrictionDim res_dim = (RestrictionDim)restriction;
                RestrictionDimDTO dto = new RestrictionDimDTO();
                dto.combination = combination;
                dto.description = res_dim.description;
                dto.RestrictionId = res_dim.RestrictionId;
                dto.x = res_dim.x;
                dto.y = res_dim.y;
                dto.z = res_dim.z;
                return Ok(dto);
            }

            if (restriction is RestrictionMat)
            {
                RestrictionMat res_mat = (RestrictionMat)restriction;
                RestrictionMatDTO dto = new RestrictionMatDTO();
                dto.combination = combination;
                dto.description = res_mat.description;
                dto.RestrictionId = res_mat.RestrictionId;
                dto.containingMaterial = res_mat.containingMaterial.name;
                dto.containedMaterial = res_mat.containedMaterial.name;
                return Ok(dto);
            }

            return BadRequest();
        }

        // POST: api/Restriction/Dimensions
        [HttpPost("Dimensions")]
        public async Task<IActionResult> PostRestrictionDim([FromBody] RestrictionDimDTO restrictionDimDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restriction = await restrictionRepository.Add(restrictionDimDTO);

            if(restriction == null){
                return BadRequest();
            }

            CombinationDTO combination = new CombinationDTO();
            combination.CombinationId = restriction.combination.CombinationId;
            combination.containedProduct = restriction.combination.containedProduct.name;
            combination.containingProduct = restriction.combination.containingProduct.name;
            combination.required = restriction.combination.required;

            RestrictionDim res_dim = (RestrictionDim)restriction;
            RestrictionDimDTO dto = new RestrictionDimDTO();
            dto.combination = combination;
            dto.description = res_dim.description;
            dto.RestrictionId = res_dim.RestrictionId;
            dto.x = res_dim.x;
            dto.y = res_dim.y;
            dto.z = res_dim.z;

            return CreatedAtAction("GetRestriction", dto);
        }

        [HttpPost("Materials")]
        public async Task<IActionResult> PostRestrictionMat([FromBody] RestrictionMatDTO restrictionMatDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restriction = await restrictionRepository.Add(restrictionMatDTO);

            if(restriction == null){
                return BadRequest();
            }

            CombinationDTO combination = new CombinationDTO();
            combination.CombinationId = restriction.combination.CombinationId;
            combination.containedProduct = restriction.combination.containedProduct.name;
            combination.containingProduct = restriction.combination.containingProduct.name;
            combination.required = restriction.combination.required;

            RestrictionMat res_mat = (RestrictionMat)restriction;
            RestrictionMatDTO dto = new RestrictionMatDTO();
            dto.combination = combination;
            dto.description = res_mat.description;
            dto.RestrictionId = res_mat.RestrictionId;
            dto.containingMaterial = res_mat.containingMaterial.name;
            dto.containedMaterial = res_mat.containedMaterial.name;

            return CreatedAtAction("GetRestriction", dto);
        }

        // DELETE: api/Restriction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestriction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restriction = await restrictionRepository.Remove(id);
            if (restriction == null)
            {
                return NotFound();
            }

            CombinationDTO combination = new CombinationDTO();
            combination.CombinationId = restriction.combination.CombinationId;
            combination.containedProduct = restriction.combination.containedProduct.name;
            combination.containingProduct = restriction.combination.containingProduct.name;
            combination.required = restriction.combination.required;

            if (restriction is RestrictionDim)
            {
                RestrictionDim res_dim = (RestrictionDim)restriction;
                RestrictionDimDTO dto = new RestrictionDimDTO();
                dto.combination = combination;
                dto.description = res_dim.description;
                dto.RestrictionId = res_dim.RestrictionId;
                dto.x = res_dim.x;
                dto.y = res_dim.y;
                dto.z = res_dim.z;
                return Ok(dto);
            }

            if (restriction is RestrictionMat)
            {
                RestrictionMat res_mat = (RestrictionMat)restriction;
                RestrictionMatDTO dto = new RestrictionMatDTO();
                dto.combination = combination;
                dto.description = res_mat.description;
                dto.RestrictionId = res_mat.RestrictionId;
                dto.containingMaterial = res_mat.containingMaterial.name;
                dto.containedMaterial = res_mat.containedMaterial.name;
                return Ok(dto);
            }

            return BadRequest();
        }
    }
}