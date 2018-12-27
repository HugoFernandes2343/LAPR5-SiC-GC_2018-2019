using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class MaterialRepository : Repository<Material, MaterialDTO>
    {

        private SiCContext context;

        public MaterialRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Material> Add(MaterialDTO dto)
        {
            if (context.Material.Any(m => m.name == dto.name)) return null;

            Material material = new Material();
            material.name = dto.name;
            material.description = dto.description;
            context.Material.Add(material);
            
            await context.SaveChangesAsync();

            return material;
        }

        public async Task<Material> Edit(int id, MaterialDTO dto)
        {
            var material = await context.Material.FindAsync(id);

            if (material == null)
            {
                return null;
            }

            if (context.Material.Any(m => m.name == dto.name && m.MaterialId != id)) return null;

            material.name = dto.name;
            material.description = dto.description;
            context.Entry(material).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return material;
        }

        public IEnumerable<Material> FindAll()
        {
            return context.Material;
        }

        public async Task<Material> FindById(int id)
        {
            return await context.Material.FindAsync(id);
        }

        public async Task<Material> Remove(int id)
        {
            var material = await context.Material.FindAsync(id);
            if (material == null) return null;

            foreach (MaterialFinishing mf in material.MaterialFinishings)
            {
                context.MaterialFinishing.Remove(mf);
            }

            foreach (ProductMaterial pm in material.ProductMaterials)
            {
                context.ProductMaterial.Remove(pm);
            }

            context.Material.Remove(material);
            await context.SaveChangesAsync();
            
            return material;
        }
    }
}