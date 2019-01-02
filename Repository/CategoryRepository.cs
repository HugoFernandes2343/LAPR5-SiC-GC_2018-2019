using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class CategoryRepository : Repository<Category, CategoryDTO>
    {
        private SiCContext context;

        public CategoryRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Category> Add(CategoryDTO dto)
        {
            Category category = dto.convertToCategory();
            List<Category> categories = category.allFathers();
            foreach (Category c in categories)
            {
                if (!context.Category.Any(e => e.name == c.name))
                {
                    return null;
                }
            }
            var father = await context.Category.SingleOrDefaultAsync(e => e.name == category.parent.name);
            category.parent = father;

            context.Category.Add(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Edit(int id, CategoryDTO dto)
        {
            var category = await context.Category.FindAsync(id);

            if (category == null) return null;

            string name = dto.name.Split(";").Last();

            if (context.Category.Any(c => c.name == name && c.CategoryId != id)) return null;

            category.name = name;
            category.description = dto.description;

            context.Entry(category).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return category;
        }

        public IEnumerable<Category> FindAll()
        {
            return context.Category;
        }

        public async Task<Category> FindById(int id)
        {
            return await context.Category.FindAsync(id);
        }

        public async Task<Category> Remove(int id)
        {
            var category = await context.Category.FindAsync(id);

            if (category == null) return null;

            if (context.Category.Any(c => c.parent.CategoryId == category.CategoryId)) return null;

            context.Category.Remove(category);
            await context.SaveChangesAsync();

            return category;
        }
    }
}