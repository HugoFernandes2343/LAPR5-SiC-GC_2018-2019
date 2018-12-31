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
    public class FactoryController : ControllerBase
    {
        private readonly FactoryRepository factoryRepository;

        public FactoryController(SiCContext context)
        {
            factoryRepository = new FactoryRepository(context);
        }

        // GET: api/Factory
        [HttpGet]
        public IEnumerable<FactoryDTO> GetFactory()
        {
            List<FactoryDTO> dtos = new List<FactoryDTO>();

            foreach (Factory f in factoryRepository.FindAll())
            {
                FactoryDTO dto = new FactoryDTO();
                dto.FactoryId = f.FactoryId;
                dto.Description = f.Description;
                CityDTO cityDTO = new CityDTO();
                City c = f.City;
                cityDTO.CityId = c.CityId;
                cityDTO.Name = c.Name;
                cityDTO.Latitude = c.Latitude;
                cityDTO.Longitude = c.Longitude;

                dto.City = cityDTO;
                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Factory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var f = await factoryRepository.FindById(id);

            if (f == null)
            {
                return NotFound();
            }

            FactoryDTO dto = new FactoryDTO();
            dto.FactoryId = f.FactoryId;
            dto.Description = f.Description;
            CityDTO cityDTO = new CityDTO();
            City c = f.City;
            cityDTO.CityId = c.CityId;
            cityDTO.Name = c.Name;
            cityDTO.Latitude = c.Latitude;
            cityDTO.Longitude = c.Longitude;

            dto.City = cityDTO;

            return Ok(dto);
        }

        // PUT: api/Factory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactory([FromRoute] int id, [FromBody] FactoryDTO factoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factoryDTO.FactoryId)
            {
                return BadRequest();
            }

            var f = await factoryRepository.Edit(id, factoryDTO);

            FactoryDTO dto = new FactoryDTO();
            dto.FactoryId = f.FactoryId;
            dto.Description = f.Description;
            CityDTO cityDTO = new CityDTO();
            City c = f.City;
            cityDTO.CityId = c.CityId;
            cityDTO.Name = c.Name;
            cityDTO.Latitude = c.Latitude;
            cityDTO.Longitude = c.Longitude;

            dto.City = cityDTO;

            return Ok(dto);
        }

        // POST: api/Factory
        [HttpPost]
        public async Task<IActionResult> PostFactory([FromBody] FactoryDTO factoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var f = await factoryRepository.Add(factoryDTO);

            if (f == null)
            {
                return BadRequest();
            }

            FactoryDTO dto = new FactoryDTO();
            dto.FactoryId = f.FactoryId;
            dto.Description = f.Description;
            CityDTO cityDTO = new CityDTO();
            City c = f.City;
            cityDTO.CityId = c.CityId;
            cityDTO.Name = c.Name;
            cityDTO.Latitude = c.Latitude;
            cityDTO.Longitude = c.Longitude;

            dto.City = cityDTO;

            return CreatedAtAction("GetFactory", dto);
        }

        // DELETE: api/Factory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var f = await factoryRepository.Remove(id);

            if (f == null)
            {
                return NotFound();
            }

            FactoryDTO dto = new FactoryDTO();
            dto.FactoryId = f.FactoryId;
            dto.Description = f.Description;
            CityDTO cityDTO = new CityDTO();
            City c = f.City;
            cityDTO.CityId = c.CityId;
            cityDTO.Name = c.Name;
            cityDTO.Latitude = c.Latitude;
            cityDTO.Longitude = c.Longitude;

            dto.City = cityDTO;

            return Ok(dto);
        }
    }
}