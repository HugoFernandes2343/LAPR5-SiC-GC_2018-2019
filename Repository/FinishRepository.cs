using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;

namespace SiC.Persistence {

    public class FinishRepository : Repository<Finish, FinishDTO>{

        private PersistenceContext _context;

        public FinishRepository(PersistenceContext context){
            _context = context;
        }

        public async Task<Finish> Add(FinishDTO dto)
        {
            Finish finish = new Finish();

            if(dto.Name != null && dto.Description != null){
                finish.Name = dto.Name;
                finish.Description = dto.Description;
            }
            else return null;

            finish.Description = dto.Description;
            
            _context.finishes.Add(finish);
            await _context.SaveChangesAsync();

            return finish;
        }

        public async Task<Finish> Edit(long id, FinishDTO dto)
        {
            var finish = await _context.finishes.FindAsync(id);

            if(finish == null) return null;

            if(dto.Name != null){
                finish.Name = dto.Name;
            }
            if(dto.Description != null){
                finish.Description = dto.Description;
            }

            _context.Entry(finish).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return finish;
        }

        public IEnumerable<Finish> FindAll()
        {
            return _context.finishes;
        }

        public async Task<Finish> FindById(long id)
        {
            return await _context.finishes.FindAsync(id);
        }

        public async Task<Finish> Remove(long id)
        {
            var finish = await _context.finishes.FindAsync(id);

            if(finish == null) return null;

            _context.finishes.Remove(finish);
            await _context.SaveChangesAsync();

            return finish;
        }
    }
}