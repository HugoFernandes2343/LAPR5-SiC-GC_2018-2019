using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.Models;

namespace SiC.Repositories
{
    public class DimensoesRepository
    {
        private readonly SiCContext _context;

        public DimensoesRepository(SiCContext context)
        {
            _context = context;
        }

        public async Task<Dimensao> findDimensao(long id)
        {
            return await _context.Dimensoes.FindAsync(id);
        }

        public async Task<int> editarDimensao(Dimensao d)
        {
            _context.Entry(d).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimensaoExists(d.Id))
                {
                    return 1;
                }
                else
                {
                    throw;
                }
            }

            return 0;
        }

        public async Task<int> guardarDimensao(Dimensao d)
        {
            _context.Dimensoes.Add(d);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> eliminarDimensoes(long id)
        {   
            Dimensao d = _context.Dimensoes.FindAsync(id).Result;
            _context.Dimensoes.Remove(d);
            return await _context.SaveChangesAsync();
        }

        public bool DimensaoExists(long id)
        {
            return _context.Dimensoes.Any(e => e.Id == id);
        }
    }

}