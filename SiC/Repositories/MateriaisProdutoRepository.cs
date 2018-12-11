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
    public class MateriaisProdutoRepository
    {
        private readonly SiCContext _context;

        public MateriaisProdutoRepository(SiCContext context)
        {
            _context = context;
        }

        public List<MateriaisProduto> findMateriaisProduto(long id)
        {
            var list = _context.MateriaisProduto.Where(s => s.ProdutoId == id).ToList();
            return list;
        }

        public async Task<int> guardarMateriaisProduto(List<Models.MateriaisProduto> list)
        {
            _context.MateriaisProduto.AddRange(list);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> eliminarMateriaisProduto(long id)
        {
            var list = _context.MateriaisProduto.Where(s=>s.ProdutoId == id).ToList();
            
            foreach(MateriaisProduto reg in list)
            {
                _context.MateriaisProduto.Remove(reg);
            }

            return await _context.SaveChangesAsync();
        }

        public bool MateriaisProdutoExists(long id)
        {
            return _context.MateriaisProduto.Any(e => e.Id == id);
        }

    }
}