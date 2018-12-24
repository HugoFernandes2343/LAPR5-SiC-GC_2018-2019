using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class DimensionRepository : Repository<Dimension, DimensionDTO>
    {
        private SiCContext context;

        public DimensionRepository(SiCContext context) 
        {
            this.context = context;
        }
        public async Task<Dimension> Add(DimensionDTO dto)
        {
            Dimension dimension = new Dimension();
            Measure depth, height, width;
            
            depth = context.Measure.SingleOrDefault(m => m.Value == dto.Depth.Value && m.ValueMax == dto.Depth.ValueMax);
            height = context.Measure.SingleOrDefault(m => m.Value == dto.Height.Value && m.ValueMax == dto.Height.ValueMax);
            width = context.Measure.SingleOrDefault(m => m.Value == dto.Width.Value && m.ValueMax == dto.Width.ValueMax);

            if(depth == null){
                if(dto.Depth.ValueMax != 0){
                    depth = new Measure(dto.Depth.Value, dto.Depth.ValueMax);
                } else {
                    depth = new Measure(dto.Depth.Value);
                }
            }

            if(height == null){
                if(dto.Height.ValueMax != 0){
                    height = new Measure(dto.Height.Value, dto.Height.ValueMax);
                } else {
                    height = new Measure(dto.Height.Value);
                }
            }

            if(width == null){
                if(dto.Width.ValueMax != 0){
                    width = new Measure(dto.Width.Value, dto.Width.ValueMax);
                } else {
                    width = new Measure(dto.Width.Value);
                }
            }

            dimension.Depth = depth;
            dimension.Height = height;
            dimension.Width = width;

            context.Dimension.Add(dimension);
            await context.SaveChangesAsync();

            return dimension;

        }

        public async Task<Dimension> Edit(int id, DimensionDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dimension> FindAll()
        {
            return context.Dimension;
        }

        public async Task<Dimension> FindById(int id)
        {
            return await context.Dimension.FindAsync(id);
        }

        public async Task<Dimension> Remove(int id)
        {
            var dimension = await context.Dimension.FindAsync(id);
            if (dimension == null)
            {
                return null;
            }

            context.Dimension.Remove(dimension);
            await context.SaveChangesAsync();
            return dimension;
        }
    }
}