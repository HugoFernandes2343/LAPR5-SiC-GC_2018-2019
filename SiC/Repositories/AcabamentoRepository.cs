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
    public class AcabamentoRepository
    {
        private readonly SiCContext _context;

        public AcabamentoRepository(SiCContext context)
        {
            _context = context;
        }

        public IEnumerable<Acabamento> getAllAcabamentos()
        {
            return _context.Acabamentos;
        }

        public async Task<int> editarAcabamento(Acabamento acabamento)
        {
            _context.Entry(acabamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcabamentoExists(acabamento.Id))
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

        public async Task<int> guardarAcabamento(Acabamento acabamento)
        {
            _context.Acabamentos.Add(acabamento);
            return await _context.SaveChangesAsync();

        }

        public long getIdByName(string acabamento)
        {
            return _context.Acabamentos.Where(s => s.Descricao == acabamento).FirstAsync().Result.Id;
        }

        public async Task<int> eliminarAcabamento(Acabamento acabamento)
        {
            _context.Acabamentos.Remove(acabamento);
            return await _context.SaveChangesAsync();
        }

        public string getAcabamentoDescricao(long acabamentoId)
        {
            return _context.Acabamentos.Find(acabamentoId).Descricao;
        }

        public async Task<Acabamento> findAcabamento(long id)
        {
            return await _context.Acabamentos.FindAsync(id);
        }

        public bool AcabamentoExists(long id)
        {
            return _context.Acabamentos.Any(e => e.Id == id);
        }

    }
}