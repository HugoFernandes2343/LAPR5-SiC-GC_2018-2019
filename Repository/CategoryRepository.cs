using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;

namespace SiC.Persistence {

    public class CategoryRepository : Repository<Category, PostCategoryDTO> {

        private PersistenceContext _context;

        public CategoryRepository(PersistenceContext context){
            _context = context;
        }

        public IEnumerable<Category> FindAll()
        {
            return _context.categories.Include(x => x.SubCategories)
                                                     .Select(f => new Category
                                                     {
                                                         Id = f.Id,
                                                         Name = f.Name,
                                                         ParentID = f.ParentID,
                                                         SubCategories = f.SubCategories
                                                     }).ToList();

        }

        public async Task<IEnumerable<Category>> FindAllAsync(){
            return await _context.categories.Select(f => new Category
                                                     {
                                                         Id = f.Id,
                                                         Name = f.Name,
                                                         ParentID = f.ParentID,
                                                         SubCategories = f.SubCategories
                                                     }).ToListAsync();
        }

        public async Task<Category> FindById(long id)
        {
            Category dbCategory = await _context.categories
                                  .Include(y => y.SubCategories)
                                  .Where(y => y.Id == id)
                                  .Select(y => new Category
                                  {
                                      Id = y.Id,
                                      Name = y.Name,
                                      ParentID = y.ParentID,
                                      SubCategories = y.SubCategories
                                  }).FirstOrDefaultAsync();

            if(dbCategory == null) return null;
            
            List<Category> sub = dbCategory.SubCategories.ToList();
            dbCategory.SubCategories = sub;

            return dbCategory;
        }

        public async Task<Category> Edit(long id, PostCategoryDTO dto)
        {
            if (dto == null) return null;

                var category = await _context.categories.FindAsync(id);

                if (category == null)
                {
                    return null;
                }
                else
                {

                if (dto.ParentID == -1)
                {
                    category.Name = dto.Name;
                }
                else if (dto.ParentID == null)
                {
                    category.Name = dto.Name;
                    category.ParentID = null;
                }
                else
                {
                    category.Name = dto.Name;
                    category.ParentID = dto.ParentID;
                }

                _context.Entry(category).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }
            
            return category;
        }

        public async Task<Category> Add(PostCategoryDTO dto)
        {
            if (dto == null) return null;

            Category category = new Category();

            if (dto.Name == null) return null;

            if (dto.ParentID == -1)
            {
                category.Name = dto.Name;
                _context.categories.Add(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                Category parentCategory = await _context.categories
                                            .Include(x => x.SubCategories)
                                            .Where(x => x.Id == dto.ParentID).FirstAsync();

                category.Name = dto.Name;
                parentCategory.SubCategories.Add(category);
                await _context.SaveChangesAsync();
                
            }

            return category;
        }

        public async Task<Category> Remove(long id)
        {
            var category = await _context.categories.FindAsync(id);

            if(category == null) return null;

            _context.categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}