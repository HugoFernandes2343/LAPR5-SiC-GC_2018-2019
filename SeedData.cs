using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiC.Models;

namespace SiC
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SiCContext(
                serviceProvider.GetRequiredService<DbContextOptions<SiCContext>>()))
            {

                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }

                Measure Depth = new Measure(20.0, 40.0);
                Measure Height = new Measure(80);
                Measure Width = new Measure(30, 50);
                Dimension Dimension = new Dimension();
                Dimension.Depth = Depth;
                Dimension.Height = Height;
                Dimension.Width = Width;

                Finishing Finishing = new Finishing();
                Finishing.name = "Polimento";
                Finishing.MaterialFinishings = new List<MaterialFinishing>();

                Material Material = new Material();
                Material.name = "Madeira";
                Material.MaterialFinishings = new List<MaterialFinishing>();
                Material.ProductMaterials = new List<ProductMaterial>();

                MaterialFinishing MF = new MaterialFinishing();
                MF.Finishing = Finishing;
                MF.Material = Material;

                Finishing.MaterialFinishings.Add(MF);
                Material.MaterialFinishings.Add(MF);

                Category Category = new Category();
                Category.name = "armario";

                Product Product = new Product();
                Product.name = "G8579";
                Product.category = Category;
                Product.ProductMaterials = new List<ProductMaterial>();
                Product.dimensions = new List<Dimension>();
                Product.dimensions.Add(Dimension);

                ProductMaterial PM = new ProductMaterial();
                PM.Material = Material;
                PM.Product = Product;

                Product.ProductMaterials.Add(PM);
                Material.ProductMaterials.Add(PM);

                context.Measure.Add(Depth);
                context.Measure.Add(Height);
                context.Measure.Add(Width);
                context.Dimension.Add(Dimension);
                context.Category.Add(Category);
                context.Finishing.Add(Finishing);
                context.Material.Add(Material);
                context.Product.Add(Product);

                MF.MaterialId = Material.MaterialId;
                MF.FinishingId = Finishing.FinishingId;

                context.MaterialFinishing.Add(MF);

                PM.MaterialId = Material.MaterialId;
                PM.ProductId = Product.ProductId;

                context.ProductMaterial.Add(PM);

                context.SaveChanges();
            }
        }
    }
}