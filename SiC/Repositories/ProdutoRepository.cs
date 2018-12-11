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
    public class ProdutoRepository
    {
        private readonly SiCContext _context;

        public ProdutoRepository(SiCContext context)
        {
            _context = context;
        }

        internal IEnumerable<Produto> getAllProdutos()
        {
             return _context.Produtos;
        }

        public async Task<Produto> findProduto(long id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> getProdutoByNome(string nome)
        {
            return await _context.Produtos.Where(s => s.Nome == nome).FirstAsync();
        }

        public async Task<int> guardarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> eliminarProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
            return await _context.SaveChangesAsync();
        }

        public bool ProdutoExists(long id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}