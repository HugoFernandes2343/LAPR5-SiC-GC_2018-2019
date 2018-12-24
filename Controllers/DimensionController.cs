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
    [Route("api/Dimension")]
    [ApiController]
    public class DimensionController : ControllerBase
    {
        private readonly DimensionRepository dimensionRepository;

        public DimensionController(SiCContext context)
        {
            dimensionRepository = new DimensionRepository(context);
        }

        // GET: api/Dimension
        [HttpGet]
        public IEnumerable<DimensionDTO> GetDimension()
        {
            List<DimensionDTO> dtos = new List<DimensionDTO>();
            foreach (Dimension dimension in dimensionRepository.FindAll())
            {
                DimensionDTO dto = new DimensionDTO();
                dto.Depth = new MeasureDTO();
                dto.Height = new MeasureDTO();
                dto.Width = new MeasureDTO();
                dto.DimensionId = dimension.DimensionId;
                dto.Depth.Id = dimension.Depth.MeasureId;
                dto.Depth.Value = dimension.Depth.Value;
                dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dto.Height.Id = dimension.Height.MeasureId;
                dto.Height.Value = dimension.Height.Value;
                dto.Height.ValueMax = dimension.Height.ValueMax;
                dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dto.Width.Id = dimension.Width.MeasureId;
                dto.Width.Value = dimension.Width.Value;
                dto.Width.ValueMax = dimension.Width.ValueMax;
                dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/Dimension/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDimension([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dimension = await dimensionRepository.FindById(id);

            if (dimension == null)
            {
                return NotFound();
            }

            DimensionDTO dto = new DimensionDTO();
            dto.Depth = new MeasureDTO();
            dto.Height = new MeasureDTO();
            dto.Width = new MeasureDTO();
            dto.DimensionId = dimension.DimensionId;
            dto.Depth.Id = dimension.Depth.MeasureId;
            dto.Depth.Value = dimension.Depth.Value;
            dto.Depth.ValueMax = dimension.Depth.ValueMax;
            dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
            dto.Height.Id = dimension.Height.MeasureId;
            dto.Height.Value = dimension.Height.Value;
            dto.Height.ValueMax = dimension.Height.ValueMax;
            dto.Height.isDiscrete = dimension.Height.isDiscrete;
            dto.Width.Id = dimension.Width.MeasureId;
            dto.Width.Value = dimension.Width.Value;
            dto.Width.ValueMax = dimension.Width.ValueMax;
            dto.Width.isDiscrete = dimension.Width.isDiscrete;

            return Ok(dto);
        }

        // PUT: api/Dimension/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimension([FromRoute] int id, [FromBody] DimensionDTO dimensionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dimensionDTO.DimensionId)
            {
                return BadRequest();
            }

            var dimension = await dimensionRepository.Edit(id, dimensionDTO);

            if (dimension == null)
            {
                return BadRequest();
            }

            DimensionDTO dto = new DimensionDTO();
            dto.Depth = new MeasureDTO();
            dto.Height = new MeasureDTO();
            dto.Width = new MeasureDTO();
            dto.DimensionId = dimension.DimensionId;
            dto.Depth.Id = dimension.Depth.MeasureId;
            dto.Depth.Value = dimension.Depth.Value;
            dto.Depth.ValueMax = dimension.Depth.ValueMax;
            dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
            dto.Height.Id = dimension.Height.MeasureId;
            dto.Height.Value = dimension.Height.Value;
            dto.Height.ValueMax = dimension.Height.ValueMax;
            dto.Height.isDiscrete = dimension.Height.isDiscrete;
            dto.Width.Id = dimension.Width.MeasureId;
            dto.Width.Value = dimension.Width.Value;
            dto.Width.ValueMax = dimension.Width.ValueMax;
            dto.Width.isDiscrete = dimension.Width.isDiscrete;

            return Ok(dto);
        }

        // POST: api/Dimension
        [HttpPost]
        public async Task<IActionResult> PostDimension([FromBody] DimensionDTO dimensionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dimension = await dimensionRepository.Add(dimensionDTO);

            if (dimension == null)
            {
                return BadRequest();
            }

            DimensionDTO dto = new DimensionDTO();
            dto.Depth = new MeasureDTO();
            dto.Height = new MeasureDTO();
            dto.Width = new MeasureDTO();
            dto.DimensionId = dimension.DimensionId;
            dto.Depth.Id = dimension.Depth.MeasureId;
            dto.Depth.Value = dimension.Depth.Value;
            dto.Depth.ValueMax = dimension.Depth.ValueMax;
            dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
            dto.Height.Id = dimension.Height.MeasureId;
            dto.Height.Value = dimension.Height.Value;
            dto.Height.ValueMax = dimension.Height.ValueMax;
            dto.Height.isDiscrete = dimension.Height.isDiscrete;
            dto.Width.Id = dimension.Width.MeasureId;
            dto.Width.Value = dimension.Width.Value;
            dto.Width.ValueMax = dimension.Width.ValueMax;
            dto.Width.isDiscrete = dimension.Width.isDiscrete;

            return CreatedAtAction("GetDimension", dto);
        }

        // DELETE: api/Dimension/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimension([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dimension = await dimensionRepository.Remove(id);
            if (dimension == null)
            {
                return NotFound();
            }

            DimensionDTO dto = new DimensionDTO();
            dto.Depth = new MeasureDTO();
            dto.Height = new MeasureDTO();
            dto.Width = new MeasureDTO();
            dto.DimensionId = dimension.DimensionId;
            dto.Depth.Id = dimension.Depth.MeasureId;
            dto.Depth.Value = dimension.Depth.Value;
            dto.Depth.ValueMax = dimension.Depth.ValueMax;
            dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
            dto.Height.Id = dimension.Height.MeasureId;
            dto.Height.Value = dimension.Height.Value;
            dto.Height.ValueMax = dimension.Height.ValueMax;
            dto.Height.isDiscrete = dimension.Height.isDiscrete;
            dto.Width.Id = dimension.Width.MeasureId;
            dto.Width.Value = dimension.Width.Value;
            dto.Width.ValueMax = dimension.Width.ValueMax;
            dto.Width.isDiscrete = dimension.Width.isDiscrete;

            return Ok(dto);
        }

    }
}