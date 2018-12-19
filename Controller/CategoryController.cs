using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiC.Model;
using SiC.Persistence;
using SiC.DTO;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly PersistenceContext _context;
        private CategoryRepository categoryRepository;

        public CategoryController(PersistenceContext context)
        {
            _context = context;
            categoryRepository = new CategoryRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody]PostCategoryDTO value)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Category category = await categoryRepository.Add(value);

                    if(category == null) return BadRequest();

                    return Ok(category);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] long id, [FromBody]PostCategoryDTO value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(value == null) return BadRequest();
                    
                    Category category = await categoryRepository.Edit(id,value);

                    if(category == null) return NotFound();

                    return Ok(category);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return StatusCode(500, new { error = ModelState.Values.SelectMany(x => x.Errors.ToList()) });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
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

            return Ok(category);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {

            if(!ModelState.IsValid) return BadRequest();

            IEnumerable<Category> temp;

            try{
                temp = await categoryRepository.FindAllAsync();
            }catch(Exception e){
                return BadRequest(e.Message);
            }

            if(temp == null) return NotFound();
            
            return Ok(temp);

        }

        /**
        Returns the category and its children by id
        e.g api/category/5
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]long id)
        {
            
            if(!ModelState.IsValid) return BadRequest();

            Category category;

            category = await categoryRepository.FindById(id);
            
            if(category == null) return NotFound();

            return Ok(category);
        }

        /**Iterates the tree and returns ALL the categories in the database and its children */
        public List<Category> GetAllLocal(List<Category> list)
        {

            int z = 0;

            List<Category> lists = new List<Category>();

            if (list.Count > 0)
            {
                lists.AddRange(list);
            }

            foreach (Category x in list)
            {
                Category dbCategory = _context.categories
                                      .Include(y => y.SubCategories)
                                      .Where(y => y.Id == x.Id)
                                      .Select(y => new Category
                                      {
                                          Id = y.Id,
                                          Name = y.Name,
                                          ParentID = y.ParentID,
                                          SubCategories = y.SubCategories
                                      }).First();

                if (dbCategory.SubCategories == null)
                {
                    z++;
                    continue;
                }

                List<Category> sub = dbCategory.SubCategories.ToList();
                dbCategory.SubCategories = GetAllLocal(sub);
                lists[z] = dbCategory;
                z++;

            }

            return lists;
        }

    }
}
