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
    public class PartesRepository
    {
        private readonly SiCContext _context;

        public PartesRepository(SiCContext context)
        {
            _context = context;
        }

        public List<Partes> findPartes(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();
            return list;
        }

        public List<Partes> findPartesObrigatorias(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();
            var list2 = new List<Partes>();

            foreach(Partes part in list){
                if(part.Obrigatoria){
                    list2.Add(part);
                }
            }

            return list2;
        }

        public List<Partes> findPartesOpcionais(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();
            var list2 = new List<Partes>();

            foreach(Partes part in list){
                if(!part.Obrigatoria){
                    list2.Add(part);
                }
            }

            return list2;
        }

        public List<Partes> findParteEm(long id)
        {
            var list = _context.Partes.Where(s => s.ParteId == id).ToList();
            return list;
        }

        public async Task<int> guardarPartes(Models.Partes partes)
        {
            _context.Partes.Add(partes);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> eliminarPartes(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();
            var list2 = _context.Partes.Where(s => s.ParteId == id).ToList();

            foreach (Partes reg in list)
            {
                _context.Partes.Remove(reg);
            }

            foreach (Partes reg in list2)
            {
                _context.Partes.Remove(reg);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> eliminarPartesObrigatorias(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();

            foreach (Partes reg in list)
            {
                if (reg.Obrigatoria)
                {
                    _context.Partes.Remove(reg);
                }

            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> eliminarPartesOpcionais(long id)
        {
            var list = _context.Partes.Where(s => s.ProdutoId == id).ToList();

            foreach (Partes reg in list)
            {
                if (!reg.Obrigatoria)
                {
                    _context.Partes.Remove(reg);
                }
            }

            return await _context.SaveChangesAsync();
        }

    }
}