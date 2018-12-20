using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;

namespace SiC.Persistence
{

    public class DimensionRepository : Repository<Dimension, DimensionDTO>
    {

        PersistenceContext _context;

        public DimensionRepository(PersistenceContext context)
        {
            this._context = context;
        }

        public async Task<Dimension> Add(DimensionDTO dto)
        {
            Dimension dimension = new Dimension();

            if (dto == null) return null;

            if (dto.Width > 0 && dto.Height > 0 && dto.Depth > 0
            && dto.WidthMax > 0 && dto.HeightMax > 0 && dto.DepthMax > 0
            && dto.WidthMax > dto.Width && dto.HeightMax > dto.Height && dto.DepthMax > dto.Depth)
            {
                var Width = new Measure(dto.Width, dto.WidthMax);

                var Height = new Measure(dto.Height, dto.HeightMax);

                var Depth = new Measure(dto.Depth, dto.DepthMax);

                await _context.measures.AddAsync(Width);
                await _context.measures.AddAsync(Height);
                await _context.measures.AddAsync(Depth);

                dimension.Width = Width;
                dimension.Height = Height;
                dimension.Depth = Depth;

            }
            else if (dto.Width > 0 && dto.Height > 0 && dto.Depth > 0)
            {
                var Width = new Measure(dto.Width);

                var Height = new Measure(dto.Height);

                var Depth = new Measure(dto.Depth);

                await _context.measures.AddAsync(Width);
                await _context.measures.AddAsync(Height);
                await _context.measures.AddAsync(Depth);

                dimension.Width = Width;
                dimension.Height = Height;
                dimension.Depth = Depth;
            }
            else
            {
                return null;
            }

            _context.dimensions.Add(dimension);
            await _context.SaveChangesAsync();

            return dimension;
        }

        public async Task<Dimension> Edit(long id, DimensionDTO dto)
        {
            var dimension = await _context.dimensions.FindAsync(id);

            if (dto == null) return null;

            if (dimension == null) return null;

            if (dto.Height > 0)
            {
                var height = await _context.measures.FindAsync(dimension.HeightId);

                height.Value = dto.Height;

                if (dto.HeightMax > 0)
                {
                    height.ValueMax = dto.HeightMax;
                    height.isDiscrete = false;
                }
                else
                {

                    if (dto.HeightMax < height.ValueMax) return null;

                    height.ValueMax = 0;
                    height.isDiscrete = true;
                }

                _context.Entry(height).State = EntityState.Modified;
            }

            if (dto.Width > 0)
            {
                var width = await _context.measures.FindAsync(dimension.WidthId);

                width.Value = dto.Height;

                if (dto.WidthMax > 0)
                {

                    if (dto.WidthMax < width.ValueMax) return null;

                    width.ValueMax = dto.WidthMax;
                    width.isDiscrete = false;
                }
                else
                {
                    width.ValueMax = 0;
                    width.isDiscrete = true;
                }
                _context.Entry(width).State = EntityState.Modified;
            }

            if (dto.Depth > 0)
            {
                var depth = await _context.measures.FindAsync(dimension.DepthId);

                depth.Value = dto.Height;

                if (dto.DepthMax > 0)
                {

                    if (dto.DepthMax < depth.ValueMax) return null;

                    depth.ValueMax = dto.DepthMax;
                    depth.isDiscrete = false;
                }
                else
                {
                    depth.ValueMax = 0;
                    depth.isDiscrete = true;
                }

                _context.Entry(depth).State = EntityState.Modified;
            }

            _context.Entry(dimension).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return dimension;
        }

        public IEnumerable<Dimension> FindAll()
        {
            return _context.dimensions;
        }

        public async Task<Dimension> FindById(long id)
        {
            var dimension = await _context.dimensions.FindAsync(id);

            if (dimension == null) return null;

            var height = await _context.measures.FindAsync(dimension.HeightId);
            var width = await _context.measures.FindAsync(dimension.WidthId);
            var depth = await _context.measures.FindAsync(dimension.DepthId);

            dimension.Height = height;
            dimension.Width = width;
            dimension.Depth = depth;

            return dimension;
        }

        public async Task<Dimension> Remove(long id)
        {
            var dimension = await _context.dimensions.FindAsync(id);

            if (dimension == null) return null;

            _context.dimensions.Remove(dimension);
            await _context.SaveChangesAsync();

            return dimension;

        }
    }
}