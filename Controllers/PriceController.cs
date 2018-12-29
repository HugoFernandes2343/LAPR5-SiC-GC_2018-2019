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
    public class PriceController : ControllerBase
    {
        private readonly PriceRepository priceRepository;

        public PriceController(SiCContext context)
        {
            priceRepository = new PriceRepository(context);
        }


        // GET: api/Price
        [HttpGet]
        public IEnumerable<PriceDTO> GetPrice()
        {
            List<PriceDTO> dtos = new List<PriceDTO>();

            foreach (Price price in priceRepository.FindAll())
            {
                PriceDTO dto = new PriceDTO();
                dto.designation = price.designation;
                dto.price = price.price;
                dto.date = price.date;
                
                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Price/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var price = await priceRepository.FindById(id);

            if (price == null)
            {
                return NotFound();
            }

            PriceDTO dto = new PriceDTO();
            dto.designation = price.designation;
            dto.price = price.price;
            dto.date = price.date;
            
            return Ok(dto);
        }

        // PUT: api/Price/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice([FromRoute] int id, [FromBody] PriceDTO priceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceDTO.PriceId)
            {
                return BadRequest();
            }

            var price = await priceRepository.Edit(id, priceDTO);

            if (price == null)
            {
                return BadRequest();
            }

            PriceDTO dto = new PriceDTO();
            dto.designation = price.designation;
            dto.price = price.price;
            dto.date = price.date;
            
            return Ok(dto);
        }

        // POST: api/Price
        [HttpPost]
        public async Task<IActionResult> PostPrice([FromBody] PriceDTO priceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var price = await priceRepository.Add(priceDTO);

            if (price == null)
            {
                return BadRequest();
            }

            PriceDTO dto = new PriceDTO();
            dto.designation = price.designation;
            dto.price = price.price;
            dto.date = price.date;

            return CreatedAtAction("GetPrice", dto);
        }

        // DELETE: api/Price/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var price = await priceRepository.Remove(id);

            if (price == null)
            {
                return BadRequest();
            }

            PriceDTO dto = new PriceDTO();
            dto.designation = price.designation;
            dto.price = price.price;
            dto.date = price.date;


            return Ok(dto);
        }

    }
}