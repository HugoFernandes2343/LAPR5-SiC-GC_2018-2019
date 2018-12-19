using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using System.Linq;
using SiC.Model;

namespace SiC.Persistence
{

    public class MaterialRepository : Repository<Material, MaterialDTO>
    {

        private PersistenceContext _context;

        public MaterialRepository(PersistenceContext context)
        {
            _context = context;
        }

        public async Task<Material> Add(MaterialDTO dto)
        {
            Material material = new Material();

            if (dto.Name != null && dto.Description != null)
            {

                material.Name = dto.Name;
                material.Description = dto.Description;

                _context.materials.Add(material);
                await _context.SaveChangesAsync();

            }
            else return null;

            return material;
        }

        public async Task<Material> Edit(long id, MaterialDTO dto)
        {
            var material = await _context.materials.FindAsync(id);

            if (material == null) return null;

            if (dto.Name != null) material.Name = dto.Name;
            if (dto.Description != null) material.Description = dto.Description;

            _context.Entry(material).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return material;

        }

        public ProductMaterial RemoveProductMaterial(EditMaterialDTO dto)
        {
            var materialProducts = _context.productMaterials.ToList();

            foreach (ProductMaterial e in materialProducts)
            {
                if (e.ProductId == dto.ProductId && e.MaterialId == dto.MaterialId)
                {
                    _context.productMaterials.Remove(e);
                    _context.SaveChanges();
                    return e;
                }
            }

            return null;
        }

        public ProductMaterial CreateProductMaterial(EditMaterialDTO dto)
        {
            if (dto == null) return null;

            ProductMaterial productMaterial = new ProductMaterial();

            if (dto.ProductId != null && dto.MaterialId != null)
            {
                productMaterial.ProductId = dto.ProductId;
                productMaterial.MaterialId = dto.MaterialId;
                _context.productMaterials.Add(productMaterial);
                _context.SaveChanges();
                return productMaterial;
            }

            return null;
        }

        public IEnumerable<Material> FindAll()
        {
            return _context.materials;
        }

        public async Task<Material> FindById(long id)
        {
            return await _context.materials.FindAsync(id);
        }

        public async Task<Material> Remove(long id)
        {
            var material = await _context.materials.FindAsync(id);

            if (material == null) return null;

            _context.materials.Remove(material);
            await _context.SaveChangesAsync();

            return material;
        }
    }
}