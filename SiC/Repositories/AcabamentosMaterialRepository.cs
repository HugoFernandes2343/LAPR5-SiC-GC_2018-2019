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
    public class AcabamentosMaterialRepository
    {
        private readonly SiCContext _context;

        public AcabamentosMaterialRepository(SiCContext context)
        {
            _context = context;
        }

        public List<AcabamentosMaterial> findAcabamentosMaterial(long id)
        {
            var list = _context.AcabamentosMaterial.Where(s => s.MaterialAcabamentoId == id).ToList();
            return list;
        }

        public async Task<int> guardarAcabamentosMaterial(Models.AcabamentosMaterial acaMat)
        {
            _context.AcabamentosMaterial.Add(acaMat);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> eliminarAcabamentosMaterial(long id)
        {
            var list = _context.AcabamentosMaterial.Where(s=>s.MaterialAcabamentoId == id).ToList();

            foreach(AcabamentosMaterial reg in list)
            {
                _context.AcabamentosMaterial.Remove(reg);
            }

            return await _context.SaveChangesAsync();
        }

        public bool AcabamentosMaterialExists(long id)
        {
            return _context.AcabamentosMaterial.Any(e => e.Id == id);
        }

    }
}