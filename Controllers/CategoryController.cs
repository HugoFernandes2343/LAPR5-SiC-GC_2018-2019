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
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryController(SiCContext context)
        {
            categoryRepository = new CategoryRepository(context);
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategory()
        {
            
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            foreach(Category category in categoryRepository.FindAll()){
                CategoryDTO dto = new CategoryDTO(category.name, category);
                dtos.Add(dto);
            }
            
            return dtos;
        }

        // GET: api/Category/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoryRepository.FindById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDTO(category.name,category));
        }

        // PUT: api/Category/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

           var category = await categoryRepository.FindById(id);

           if (category == null)
            {
                return BadRequest();
            }

            return Ok(new CategoryDTO(category.name,category));
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoryRepository.Add(categoryDTO);

            if (category == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCategory", new CategoryDTO(category.name,category));
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoryRepository.Remove(id);
            
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDTO(category.name,category));
        }

    }
}