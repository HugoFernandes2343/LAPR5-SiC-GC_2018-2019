using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class RestrictionRepository : Repository<Restriction, RestrictionDTO>
    {
        private SiCContext context;

        public RestrictionRepository(SiCContext context) 
        {
            this.context = context;
        }
        public async Task<Restriction> Add(RestrictionDTO dto)
        {
            Combination combination = await context.Combination.SingleOrDefaultAsync(c => c.containedProduct.name == dto.combination.containedProduct && c.containingProduct.name == dto.combination.containingProduct);

            if (combination == null)
            {
                return null;
            }

            if (dto is RestrictionDimDTO)
            {
                RestrictionDimDTO res_dim_dto = (RestrictionDimDTO)dto;
                RestrictionDim res_dim = new RestrictionDim();
                res_dim.combination = combination;
                res_dim.x = res_dim_dto.x;
                res_dim.y = res_dim_dto.y;
                res_dim.z = res_dim_dto.z;

                context.Restriction.Add(res_dim);
                await context.SaveChangesAsync();

                return res_dim;
            }

            if (dto is RestrictionMatDTO)
            {
                RestrictionMatDTO res_mat_dto = (RestrictionMatDTO)dto;
                RestrictionMat res_mat = new RestrictionMat();
                Material containingMaterial = await context.Material.SingleOrDefaultAsync(m => m.name == res_mat_dto.containingMaterial);
                Material containedMaterial = await context.Material.SingleOrDefaultAsync(m => m.name == res_mat_dto.containedMaterial);
                
                if(containedMaterial == null) return null;
                if(containingMaterial == null) return null;

                res_mat.combination = combination;
                res_mat.containingMaterial = containingMaterial;
                res_mat.containedMaterial = containedMaterial;

                context.Restriction.Add(res_mat);
                await context.SaveChangesAsync();

                return res_mat;
            }

            return null;
        }

        public async Task<Restriction> Edit(int id, RestrictionDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Restriction> FindAll()
        {
            return context.Restriction;
        }

        public async Task<Restriction> FindById(int id)
        {
            return await context.Restriction.FindAsync(id);
        }

        public async Task<Restriction> Remove(int id)
        {
            var restriction = await context.Restriction.FindAsync(id);
            if (restriction == null)
            {
                return null;
            }

            context.Restriction.Remove(restriction);
            await context.SaveChangesAsync();
            return restriction;
        }
    }
}