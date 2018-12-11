using Microsoft.EntityFrameworkCore;

namespace SiC.Models
{
    public class SiCContext : DbContext
    {
        public SiCContext(DbContextOptions<SiCContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Dimensao> Dimensoes { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Material> Materiais { get; set; }

        public DbSet<Acabamento> Acabamentos { get; set; }

        public DbSet<MateriaisProduto> MateriaisProduto { get; set;}
        
        public DbSet<AcabamentosMaterial> AcabamentosMaterial { get; set; }

        public DbSet<MaterialAcabamento> MateriaisAcabamentos { get; set; }

        public DbSet<Partes> Partes { get; set; }

        public DbSet<SubCategoria> SubCategorias { get; set; } 
    }
}