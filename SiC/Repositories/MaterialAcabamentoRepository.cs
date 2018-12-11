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
    public class MaterialAcabamentoRepository
    {
        private readonly SiCContext _context;

        public MaterialAcabamentoRepository(SiCContext context)
        {
            _context = context;
        }

        internal IEnumerable<MaterialAcabamento> getAllMateriaisAcabamentos()
        {
             return _context.MateriaisAcabamentos;
        }

        public async Task<MaterialAcabamento> findMaterialAcabamento(long id)
        {
            return await _context.MateriaisAcabamentos.FindAsync(id);
        }

        public async Task<int> editarMaterialAcabamento(MaterialAcabamento materialAcabamento)
        {
            _context.Entry(materialAcabamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialAcabamentoExists(materialAcabamento.Id))
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

        public async Task<int> guardarMaterialAcabamento(MaterialAcabamento materialAcabamento)
        {
            _context.MateriaisAcabamentos.Add(materialAcabamento);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> eliminarMaterialAcabamento(MaterialAcabamento materialAcabamento)
        {
            _context.MateriaisAcabamentos.Remove(materialAcabamento);
            return await _context.SaveChangesAsync();
        }

        public async Task<MaterialAcabamento> findMaterialAcabamentoByName(string nome)
        {
            return await _context.MateriaisAcabamentos.Where(s => s.Nome == nome).FirstAsync();
        }

        public bool MaterialAcabamentoExists(long id)
        {
            return _context.MateriaisAcabamentos.Any(e => e.Id == id);
        }
    }
}