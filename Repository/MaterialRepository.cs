using System;
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
            material.MaterialFinishings = new List<MaterialFinishing>();
            material.description = dto.description;
            material.Prices = new List<Price>();
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

            foreach (Price pr in material.Prices)
            {
                context.Price.Remove(pr);
            }

            context.Material.Remove(material);
            await context.SaveChangesAsync();
            
            return material;
        }

        internal async Task<Material> AddMaterialFinishing(int id, int idf)
        {
            var material = await context.Material.FindAsync(id);
            var finishing = await context.Finishing.FindAsync(idf);

            if(material == null || finishing == null) return null;

            if(context.MaterialFinishing.Any(mfs => mfs.FinishingId == idf && mfs.MaterialId == id)) return null;

            MaterialFinishing mf = new MaterialFinishing();
            mf.FinishingId = finishing.FinishingId;
            mf.Finishing = finishing;
            mf.MaterialId = material.MaterialId;
            mf.Material = material;

            finishing.MaterialFinishings.Add(mf);
            material.MaterialFinishings.Add(mf);

            
            context.Entry(material).State = EntityState.Modified;
            context.Entry(finishing).State = EntityState.Modified;
            context.MaterialFinishing.Add(mf);

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

        internal async Task<Material> RemoveMaterialFinishing(int id, int idf)
        {
            var finishing = await context.Product.FindAsync(id);
            var material = await context.Material.FindAsync(idf);
            MaterialFinishing mfs = null;

            if (finishing == null || material == null) return null;

            foreach(MaterialFinishing mf in material.MaterialFinishings){
                if(mf.MaterialId == idf){
                    mfs = mf;
                }
            }

            if (mfs == null) return null;

            context.MaterialFinishing.Remove(mfs);
            await context.SaveChangesAsync();

            return material;
        }


        internal async Task<Material> AddPrice(int id, int idd)
        {
            var material = await context.Material.FindAsync(id);
            var price = await context.Price.FindAsync(idd);

            if (material == null || price == null) return null;

            if (material.Prices.Contains(price)) return null;

            material.Prices.Add(price);

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
    }
}