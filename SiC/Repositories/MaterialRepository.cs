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
    public class MaterialRepository
    {
        private readonly SiCContext _context;

        public MaterialRepository(SiCContext context)
        {
            _context = context;
        }

        internal IEnumerable<Material> getAllMateriais()
        {
             return _context.Materiais;
        }

        public async Task<Material> findMaterial(long id)
        {
            return await _context.Materiais.FindAsync(id);
        }

        public async Task<int> editarMaterial(Material material)
        {
            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
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

        public long getIdByName(string material)
        {
            return _context.Materiais.Where(s => s.Descricao == material).FirstAsync().Result.Id;
        }

        public async Task<int> guardarMaterial(Material material)
        {
            _context.Materiais.Add(material);
            return await _context.SaveChangesAsync();
        }

        public string getMaterialDescricao(long materialId)
        {
            return _context.Materiais.Find(materialId).Descricao;
        }

        public async Task<int> eliminarMaterial(Material material)
        {
            _context.Materiais.Remove(material);
            return await _context.SaveChangesAsync();
        }

        public bool MaterialExists(long id)
        {
            return _context.Materiais.Any(e => e.Id == id);
        }
    }
}