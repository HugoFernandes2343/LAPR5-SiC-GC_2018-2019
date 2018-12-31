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
    public class CityController : ControllerBase
    {
        private readonly CityRepository cityRepository;

        public CityController(SiCContext context)
        {
            cityRepository = new CityRepository(context);
        }

        // GET: api/City
        [HttpGet]
        public IEnumerable<CityDTO> GetCity()
        {
            List<CityDTO> dtos = new List<CityDTO>();

            foreach(City c in cityRepository.FindAll())
            {
                CityDTO dto = new CityDTO();
                dto.CityId = c.CityId;
                dto.Name = c.Name;
                dto.Latitude = c.Latitude;
                dto.Longitude = c.Longitude;
                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var c = await cityRepository.FindById(id);

            if (c == null)
            {
                return NotFound();
            }

            CityDTO dto = new CityDTO();
            dto.CityId = c.CityId;
            dto.Name = c.Name;
            dto.Latitude = c.Latitude;
            dto.Longitude = c.Longitude;

            return Ok(dto);
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity([FromRoute] int id, [FromBody] CityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cityDTO.CityId)
            {
                return BadRequest();
            }

            var city = await cityRepository.Edit(id, cityDTO);

            CityDTO dto = new CityDTO();
            dto.CityId = city.CityId;
            dto.Name = city.Name;
            dto.Latitude = city.Latitude;
            dto.Longitude = city.Longitude;

            return Ok(dto);
        }

        // POST: api/City
        [HttpPost]
        public async Task<IActionResult> PostCity([FromBody] CityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = await cityRepository.Add(cityDTO);

            if(city == null)
            {
                return BadRequest();
            }

            CityDTO dto = new CityDTO();
            dto.CityId = city.CityId;
            dto.Name = city.Name;
            dto.Latitude = city.Latitude;
            dto.Longitude = city.Longitude;

            return CreatedAtAction("GetCity", dto);
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = await cityRepository.Remove(id);

            if (city == null)
            {
                return BadRequest();
            }

            CityDTO dto = new CityDTO();
            dto.CityId = city.CityId;
            dto.Name = city.Name;
            dto.Latitude = city.Latitude;
            dto.Longitude = city.Longitude;

            return Ok(dto);
        }
    }
}