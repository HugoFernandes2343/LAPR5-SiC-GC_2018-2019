using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.Models;

namespace SiC.Models
{
    public class SiCContext : DbContext
    {
        public SiCContext(DbContextOptions<SiCContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restriction>()
                        .HasDiscriminator<string>("type")
                        .HasValue<RestrictionDim>("RDIM")
                        .HasValue<RestrictionMat>("RMAT");

            modelBuilder.Entity<ProductMaterial>()
                        .HasKey(pm => new { pm.ProductId, pm.MaterialId });
            modelBuilder.Entity<ProductMaterial>()
                        .HasOne(pm => pm.Product)
                        .WithMany(p => p.ProductMaterials)
                        .HasForeignKey(pm => pm.ProductId);
            modelBuilder.Entity<ProductMaterial>()
                        .HasOne(pm => pm.Material)
                        .WithMany(m => m.ProductMaterials)
                        .HasForeignKey(pm => pm.MaterialId);

            modelBuilder.Entity<MaterialFinishing>()
                        .HasKey(mf => new { mf.MaterialId, mf.FinishingId });
            modelBuilder.Entity<MaterialFinishing>()
                        .HasOne(mf => mf.Material)
                        .WithMany(m => m.MaterialFinishings)
                        .HasForeignKey(mf => mf.MaterialId);
            modelBuilder.Entity<MaterialFinishing>()
                        .HasOne(mf => mf.Finishing)
                        .WithMany(f => f.MaterialFinishings)
                        .HasForeignKey(mf => mf.FinishingId);

            modelBuilder.Entity<CollectionProduct>()
                        .HasKey(cp => new {cp.CollectionId,cp.ProdutctId});
            modelBuilder.Entity<CollectionProduct>()
                        .HasOne(cp => cp.Collection)
                        .WithMany(c => c.CollectionProducts)
                        .HasForeignKey(cp => cp.CollectionId);
            modelBuilder.Entity<CollectionProduct>()
                        .HasOne(cp => cp.Product)
                        .WithMany(p => p.CollectionProducts)
                        .HasForeignKey(cp => cp.ProdutctId);
                
            modelBuilder.Entity<CatalogProduct>()
                        .HasKey(cp => new { cp.CatalogId, cp.ProductId });
            modelBuilder.Entity<CatalogProduct>()
                        .HasOne(cp => cp.Catalog)
                        .WithMany(c => c.CatalogProducts)
                        .HasForeignKey(cp => cp.CatalogId);
            modelBuilder.Entity<CatalogProduct>()
                        .HasOne(cp => cp.Product)
                        .WithMany(p => p.CatalogProducts)
                        .HasForeignKey(cp => cp.ProductId);
        }
        public DbSet<SiC.Models.Product> Product { get; set; }
        public DbSet<SiC.Models.Material> Material { get; set; }
        public DbSet<SiC.Models.Category> Category { get; set; }
        public DbSet<SiC.Models.Dimension> Dimension { get; set; }
        public DbSet<SiC.Models.Measure> Measure { get; set; }
        public DbSet<SiC.Models.Combination> Combination { get; set; }
        public DbSet<SiC.Models.Restriction> Restriction { get; set; }
        public DbSet<SiC.Models.Catalog> Catalog { get; set; }
        public DbSet<SiC.Models.Finishing> Finishing { get; set; }
        public DbSet<SiC.Models.MaterialFinishing> MaterialFinishing { get; set; }
        public DbSet<SiC.Models.ProductMaterial> ProductMaterial { get; set; }
        public DbSet<SiC.Models.CatalogProduct> CatalogProduct { get; set; }
        public DbSet<SiC.Models.Price> Price { get; set; }
        public DbSet<SiC.Models.Factory> Factory { get; set; }
        public DbSet<SiC.Models.City> City { get; set; }
        public DbSet<SiC.Models.Order> Order { get; set; }
        public DbSet<SiC.Models.Collection> Collection { get; set; }
        public DbSet<SiC.Models.CollectionProduct> CollectionProduct { get; set; }

    }
}
