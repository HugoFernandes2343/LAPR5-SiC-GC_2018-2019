using Microsoft.EntityFrameworkCore;
using SiC.Model;

namespace SiC.Persistence
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options)
            : base(options)
        {
        }

        public DbSet<Catalog> catalogs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Dimension> dimensions { get; set; }
        public DbSet<Finish> finishes { get; set; }
        public DbSet<Material> materials { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Restriction> restrictions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Measure> measures { get; set; }
        public DbSet<OrderAndProduct> ordersAndProducts { get; set; }
        public DbSet<ProductMaterial> productMaterials { get; set; }
        public DbSet<MaterialFinish> materialFinishes { get; set; }
        public DbSet<ChildMaterialRestriction> childMaterialRestrictions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
            .HasMany(p => p.SubCategories)
            .WithOne(p => p.ParentCategory)
            .HasForeignKey(p => p.ParentID);

            builder.Entity<Product>()
            .HasMany(p => p.Components)
            .WithOne(p => p.ParentProduct)
            .HasForeignKey(p => p.ParentId);
        }

    }


}