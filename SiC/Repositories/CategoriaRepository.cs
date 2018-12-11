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
    public class CategoriaRepository
    {
        private readonly SiCContext _context;

        public CategoriaRepository(SiCContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> getAllCategorias()
        {
            return _context.Categorias;
        }

        public async Task<int> editarSuperCategoria(long id)
        {
            List<Categoria> list = _context.Categorias.Where(s => s.SuperCategoriaId == id).ToList();
            foreach(Categoria cat in list)
            {
                await eliminarCategoria(cat);
                cat.SuperCategoriaId = 0;
                await guardarCategoria(cat);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> editarCategoria(Models.Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(categoria.Id))
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

        public async Task<int> guardarCategoria(Models.Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> eliminarCategoria(Models.Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            return await _context.SaveChangesAsync();
        }

        public async Task<Categoria> findCategoria(long id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public long getIdByName(string categoria)
        {
            return _context.Categorias.Where(s => s.Descricao == categoria).FirstAsync().Result.Id;
        }

        public bool CategoriaExists(long id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }

    }
}