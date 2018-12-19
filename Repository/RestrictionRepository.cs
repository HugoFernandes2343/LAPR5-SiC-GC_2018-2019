using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;

namespace SiC.Persistence
{
    public class RestrictionRepository : Repository<Restriction, RestrictionDTO>
    {
        private PersistenceContext context;

        public RestrictionRepository(PersistenceContext context)
        {
            this.context = context;
        }

        public async Task<Restriction> Add(RestrictionDTO dto)
        {
            Restriction restriction = new Restriction();

            //validation of restrictions attributes..

            context.restrictions.Add(restriction);
            await context.SaveChangesAsync();

            return restriction;
        }

        public async Task<Restriction> Edit(long id, RestrictionDTO dto)
        {
            var restriction = await context.restrictions.FindAsync(id);

            if (restriction == null) return null;

            context.Entry(restriction).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return restriction;

        }

        public IEnumerable<Restriction> FindAll()
        {
            return context.restrictions;
        }

        public async Task<Restriction> FindById(long id)
        {
            var restriction = await context.restrictions.FindAsync(id);

            List<ChildMaterialRestriction> childMaterialRestrictions =
                await context.childMaterialRestrictions
                .Where(c => c.RestrictionId == id).ToListAsync();

            List<Material> materials = await context.materials.ToListAsync();

            List<Material> childMaterials = new List<Material>();

            foreach(ChildMaterialRestriction c in childMaterialRestrictions){
                foreach(Material material in materials){
                    
                    if(c.MaterialId == material.Id){
                        childMaterials.Add(material);
                    }

                }
            }

            restriction.childMaterialRestrictions = childMaterials;

            return restriction;

        }

        public async Task<Restriction> Remove(long id)
        {
            var restriction = await context.restrictions.FindAsync(id);

            if (restriction == null) return null;

            context.restrictions.Remove(restriction);
            await context.SaveChangesAsync();

            return restriction;


        }
    }
}