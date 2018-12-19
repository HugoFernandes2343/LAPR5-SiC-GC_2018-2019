using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controller;
using SiC.Model;
using SiC.Persistence;
using Xunit;
using Moq;
using SiC.DTO;

namespace SiC.Test
{
    public class ProductControllerTest
    {

        PersistenceContext context;
        ProductController controller;

        public ProductControllerTest()
        {

            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Product> products = new List<Product>();

            foreach (Product product in context.products)
            {
                products.Add(product);
            }

            if (products.Count == 0)
            {
                Measure height = new Measure(100);
                Measure width = new Measure(100);
                Measure depth = new Measure(100);

                Dimension d = new Dimension();
                d.Width = width;
                d.Height = height;
                d.Depth = depth;

                Category c = new Category();
                c.Name = "category";
                Restriction r = new Restriction();

                Product product = new Product();
                product.Name = "product1";
                product.Price = 500;
                product.DimensionId = 1;
                product.RestrictionId = 1;
                product.CategoryId = 1;

                Product product2 = new Product();
                product2.Name = "prod2";
                product2.Price = 100;
                product2.ParentId = 1;

                context.measures.Add(height);
                context.measures.Add(width);
                context.measures.Add(depth);
                context.dimensions.Add(d);
                context.categories.Add(c);
                context.restrictions.Add(r);
                context.products.Add(product);
                context.products.Add(product2);
                context.SaveChanges();
            }

            controller = new ProductController(context);

        }


        [Fact]
        public async void TestPutProduct()
        {
            PostProductDTO dto = new PostProductDTO();
            dto.Name = "dummy";
            dto.Price = 100;


            var result = await controller.PutProduct((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutProduct((long)1000, dto);
            Assert.IsType<NotFoundResult>(result2);

            var result3 = await controller.PutProduct((long)1, null);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async Task TestPostProduct()
        {
            PostProductDTO dto = new PostProductDTO();
            dto.Name = "Closet";
            dto.Description = "Default Desc";
            dto.Price = 50;
            dto.DimensionId = 1;
            dto.CategoryId = 1;
            dto.RestrictionId = 1;
            dto.ParentId = -1;

            //Should work
            var result = await controller.PostProduct(dto);
            Assert.IsType<OkObjectResult>(result);

            var result3 = await controller.PostProduct(null);
            Assert.IsType<BadRequestResult>(result3);
        }

        //TODO
        [Fact]
        public async void TestGetProductByName()
        {
            string name = "product1";
            string wrongName = "asdas";

            var result = await controller.GetProductByName(name);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.GetProductByName(wrongName);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void TestGetParentProduct()
        {
            var result = await controller.getParentProductLocal((long)1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestDeleteProduct()
        {
            PostProductDTO dto = new PostProductDTO();
            dto.Name = "Le Produit";

            await controller.PostProduct(dto);

            List<Product> products = new List<Product>();

            foreach (Product c in context.products)
            {
                products.Add(c);
            }

            var result = await controller.DeleteProduct(products.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteProduct((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }
    }
}
