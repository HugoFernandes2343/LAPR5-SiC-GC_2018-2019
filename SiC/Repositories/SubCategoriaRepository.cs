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
    public class SubCategoriaRepository
    {
        private readonly SiCContext _context;

        public SubCategoriaRepository(SiCContext context)
        {
            _context = context;
        }

        public List<SubCategoria> findSubCategorias(long id)
        {
            var list = _context.SubCategorias.Where(s => s.CategoriaId == id).ToList();
            return list;
        }

        public async Task<int> guardarSubCategoria(Models.SubCategoria subCategoria)
        {
            _context.SubCategorias.Add(subCategoria);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> eliminarSubCategorias(long id)
        {
            var list = _context.SubCategorias.Where(s=>s.CategoriaId == id).ToList();
            var list2 = _context.SubCategorias.Where(s=>s.SubCatId == id).ToList();

            foreach(SubCategoria reg in list)
            {
                _context.SubCategorias.Remove(reg);
            }

            foreach(SubCategoria reg in list2)
            {
                _context.SubCategorias.Remove(reg);
            }

            return await _context.SaveChangesAsync();
        }

        public bool SubCategoriaExists(long id)
        {
            return _context.SubCategorias.Any(e => e.Id == id);
        }

    }
}