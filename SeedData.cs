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

                City c = new City("Aveiro", 40.641190, -8.653620);
                City c1 = new City("Beja", 38.015621, -7.865230);
                City c2 = new City("Braga", 41.545448, -8.426507);
                City c3 = new City("Braganca", 41.806114, -6.756738);
                City c4 = new City("Castelo Branco", 39.819714, -7.496466);
                City c5 = new City("Coimbra", 40.203316, -8.410257);
                City c6 = new City("Evora", 38.571430, -7.913502);
                City c7 = new City("Faro", 37.015362, -7.935110);
                City c8 = new City("Guarda", 40.537128, -7.267850);
                City c9 = new City("Leiria", 39.749535, -8.807683);
                City c10 = new City("Lisboa", 38.722252, -9.139337);
                City c11 = new City("Portalegre", 39.296707, -7.428476);
                City c12 = new City("Porto", 41.157944, -8.629105);
                City c13 = new City("Santarem", 39.236179, -8.687080);
                City c14 = new City("Setubal", 38.525406, -8.894100);
                City c15 = new City("Viana do Castelo", 41.691807, -8.834451);
                City c16 = new City("Vila Real", 41.295898, -7.746350);
                City c17 = new City("Viseu", 40.656586, -7.912471);

                Factory Factory = new Factory();
                Factory.City = c12;
                Factory.Description = "Fabrica situada no Porto";

                Measure Depth = new Measure(20.0, 40.0);
                Measure Height = new Measure(80);
                Measure Width = new Measure(30, 50);

                Measure Depth_Part = new Measure(10.0, 20.0);
                Measure Height_Part = new Measure(40);
                Measure Width_Part = new Measure(15, 25);

                Dimension Dimension_Product = new Dimension();
                Dimension_Product.Depth = Depth;
                Dimension_Product.Height = Height;
                Dimension_Product.Width = Width;

                Dimension Dimension_Product_Part_A1 = new Dimension();
                Dimension_Product_Part_A1.Depth = Depth_Part;
                Dimension_Product_Part_A1.Height = Height_Part;
                Dimension_Product_Part_A1.Width = Width_Part;

                Dimension Dimension_Product_Part_A2 = new Dimension();
                Dimension_Product_Part_A2.Depth = Depth_Part;
                Dimension_Product_Part_A2.Height = Height_Part;
                Dimension_Product_Part_A2.Width = Width_Part;

                Price Finishing_Price = new Price();
                Finishing_Price.date = DateTime.Parse("2019-01-06");
                Finishing_Price.designation = "Polimento";
                Finishing_Price.price = 2.50;

                Price Material_Price = new Price();
                Material_Price.date = DateTime.Parse("2019-01-06");
                Material_Price.designation = "Madeira";
                Material_Price.price = 5.0;

                Finishing Finishing = new Finishing();
                Finishing.name = "Polimento";
                Finishing.description = "Acabamento que confere brilho e protecao do material quando aplicado";
                Finishing.MaterialFinishings = new List<MaterialFinishing>();
                Finishing.Prices = new List<Price>();

                Material Material = new Material();
                Material.name = "Madeira";
                Material.description = "Madeira de pinho de elevada durabilidade";
                Material.MaterialFinishings = new List<MaterialFinishing>();
                Material.ProductMaterials = new List<ProductMaterial>();
                Material.Prices = new List<Price>();

                Category Category = new Category();
                Category.name = "armario";
                Category.description = "Esta categoria representa o topo da taxonomia";

                Category Category_Part = new Category();
                Category_Part.name = "gaveta";
                Category_Part.description = "Categoria desenvolvida para teste";
                Category_Part.parent = Category;

                Product Product = new Product();
                Product.name = "G8579";
                Product.description = "Produto desenvolvido para teste";
                Product.category = Category;
                Product.ProductMaterials = new List<ProductMaterial>();
                Product.CatalogProducts = new List<CatalogProduct>();
                Product.CollectionProducts = new List<CollectionProduct>();
                Product.dimensions = new List<Dimension>();
                Product.dimensions.Add(Dimension_Product);

                Product Product_Part1 = new Product();
                Product_Part1.name = "G8579_Part_A1";
                Product_Part1.description = "Produto desenvolvido para teste";
                Product_Part1.category = Category_Part;
                Product_Part1.ProductMaterials = new List<ProductMaterial>();
                Product_Part1.CatalogProducts = new List<CatalogProduct>();
                Product_Part1.CollectionProducts = new List<CollectionProduct>();
                Product_Part1.dimensions = new List<Dimension>();
                Product_Part1.dimensions.Add(Dimension_Product_Part_A1);

                Product Product_Part2 = new Product();
                Product_Part2.name = "G8579_Part_A2";
                Product_Part2.description = "Produto desenvolvido para teste";
                Product_Part2.category = Category_Part;
                Product_Part2.ProductMaterials = new List<ProductMaterial>();
                Product_Part2.CatalogProducts = new List<CatalogProduct>();
                Product_Part2.CollectionProducts = new List<CollectionProduct>();
                Product_Part2.dimensions = new List<Dimension>();
                Product_Part2.dimensions.Add(Dimension_Product_Part_A2);

                Combination Combination_P_P1 = new Combination(Product, Product_Part1, true);

                Combination Combination_P_P2 = new Combination(Product, Product_Part2, false);

                Catalog Catalog = new Catalog();
                Catalog.CatalogName = "Catalogo G8579";
                Catalog.CatalogDescription = "Catalogo desenvolvido para teste";
                Catalog.Date = "2019-01-06";
                Catalog.CatalogProducts = new List<CatalogProduct>();

                Collection Collection = new Collection();
                Collection.collectionName = "Madeira e mais Madeira";
                Collection.aestheticParameter = "Madeira";
                Collection.CollectionProducts = new List<CollectionProduct>();


                MaterialFinishing MF = new MaterialFinishing();
                MF.Finishing = Finishing;
                MF.Material = Material;

                Finishing.MaterialFinishings.Add(MF);
                Material.MaterialFinishings.Add(MF);

                ProductMaterial PM = new ProductMaterial();
                PM.Material = Material;
                PM.Product = Product;

                Product.ProductMaterials.Add(PM);
                Material.ProductMaterials.Add(PM);

                CatalogProduct CatP = new CatalogProduct();
                CatP.Catalog = Catalog;
                CatP.Product = Product;

                Product.CatalogProducts.Add(CatP);
                Catalog.CatalogProducts.Add(CatP);

                CollectionProduct ColP = new CollectionProduct();
                ColP.Collection = Collection;
                ColP.Product = Product;

                Product.CollectionProducts.Add(ColP);
                Collection.CollectionProducts.Add(ColP);

                context.City.Add(c);
                context.City.Add(c1);
                context.City.Add(c2);
                context.City.Add(c3);
                context.City.Add(c4);
                context.City.Add(c5);
                context.City.Add(c6);
                context.City.Add(c7);
                context.City.Add(c8);
                context.City.Add(c9);
                context.City.Add(c10);
                context.City.Add(c11);
                context.City.Add(c12);
                context.City.Add(c13);
                context.City.Add(c14);
                context.City.Add(c15);
                context.City.Add(c16);
                context.City.Add(c17);

                context.Factory.Add(Factory);

                context.Measure.Add(Depth);
                context.Measure.Add(Height);
                context.Measure.Add(Width);
                context.Measure.Add(Depth_Part);
                context.Measure.Add(Height_Part);
                context.Measure.Add(Width_Part);

                context.Dimension.Add(Dimension_Product);
                context.Dimension.Add(Dimension_Product_Part_A1);
                context.Dimension.Add(Dimension_Product_Part_A2);

                context.Price.Add(Material_Price);
                context.Price.Add(Finishing_Price);

                context.Finishing.Add(Finishing);
                context.Material.Add(Material);

                context.Category.Add(Category);
                context.Category.Add(Category_Part);

                context.Product.Add(Product);
                context.Product.Add(Product_Part1);
                context.Product.Add(Product_Part2);

                context.Combination.Add(Combination_P_P1);
                context.Combination.Add(Combination_P_P2);

                context.Catalog.Add(Catalog);

                context.Collection.Add(Collection);

                MF.MaterialId = Material.MaterialId;
                MF.FinishingId = Finishing.FinishingId;

                context.MaterialFinishing.Add(MF);

                PM.MaterialId = Material.MaterialId;
                PM.ProductId = Product.ProductId;

                context.ProductMaterial.Add(PM);

                CatP.CatalogId = Catalog.CatalogId;
                CatP.ProductId = Product.ProductId;

                context.CatalogProduct.Add(CatP);

                ColP.CollectionId = Collection.CollectionId;
                ColP.ProdutctId = Product.ProductId;

                context.CollectionProduct.Add(ColP);

                context.SaveChanges();
            }
        }
    }
}