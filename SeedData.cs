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

                City c = new City("Aveiro", 40.641190, -8.653620);
                context.City.Add(c);
                City c1 = new City("Beja", 38.015621, -7.865230);
                context.City.Add(c1);
                City c2 = new City("Braga", 41.545448, -8.426507);
                context.City.Add(c2);
                City c3 = new City("Bragança", 41.806114, -6.756738);
                context.City.Add(c3);
                City c4 = new City("Castelo Branco", 39.819714, -7.496466);
                context.City.Add(c4);
                City c5 = new City("Coimbra", 40.203316, -8.410257);
                context.City.Add(c5);
                City c6 = new City("Évora", 38.571430, -7.913502);
                context.City.Add(c6);
                City c7 = new City("Faro", 37.015362, -7.935110);
                context.City.Add(c7);
                City c8 = new City("Guarda", 40.537128, -7.267850);
                context.City.Add(c8);
                City c9 = new City("Leiria", 39.749535, -8.807683);
                context.City.Add(c9);
                City c10 = new City("Lisboa", 38.722252, -9.139337);
                context.City.Add(c10);
                City c11 = new City("Portalegre", 39.296707, -7.428476);
                context.City.Add(c11);
                City c12 = new City("Porto", 41.157944, -8.629105);
                context.City.Add(c12);
                City c13 = new City("Santarém", 39.236179, -8.687080);
                context.City.Add(c13);
                City c14 = new City("Setúbal", 38.525406, -8.894100);
                context.City.Add(c14);
                City c15 = new City("Viana do Castelo", 41.691807, -8.834451);
                context.City.Add(c15);
                City c16 = new City("Vila Real", 41.295898, -7.746350);
                context.City.Add(c16);
                City c17 = new City("Vila Real", 41.295898, -7.746350);
                context.City.Add(c17);


                context.SaveChanges();
            }
        }
    }
}