using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controller;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;
using Xunit;

namespace SiC.Test
{
    public class CatalogControllerTest
    {

        PersistenceContext context;
        CatalogController controller;

        public CatalogControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Catalog> catalogs = new List<Catalog>();

            foreach (Catalog c in context.catalogs)
            {
                catalogs.Add(c);
            }

            if (catalogs.Count == 0)
            {

                Catalog catalog = new Catalog();
                catalog.Date = "10/10/2018";

                context.catalogs.Add(catalog);
                context.SaveChanges();

            }
            controller = new CatalogController(context);
        }

        [Fact]
        public async void TestGetCatalogById()
        {
            //Should find
            var result = await controller.GetCatalogById((long)1);
            Assert.IsType<OkObjectResult>(result);

            //Should not find
            var result2 = await controller.GetCatalogById((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void TestPutCatalog()
        {
            CatalogDTO dto = new CatalogDTO();
            dto.Date = "10/15/2018";

            var result = await controller.PutCatalog((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutCatalog((long)1, null);
            Assert.IsType<BadRequestResult>(result2);

            var result3 = await controller.PutCatalog((long)1000, dto);
            Assert.IsType<NotFoundResult>(result3);
        }

        [Fact]
        public async void TestPostCatalog()
        {

            CatalogDTO dto = new CatalogDTO();
            dto.Date = "10/12/2018";

            var result = await controller.PostCatalog(dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PostCatalog(null);
            Assert.IsType<BadRequestResult>(result2);

            CatalogDTO dtoDateNull = new CatalogDTO();
            var result3 = await controller.PostCatalog(dtoDateNull);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async void DeleteCatalogTest()
        {
            CatalogDTO c = new CatalogDTO();
            c.Date = "10/10/10";
            await controller.PostCatalog(c);

            List<Catalog> cs = new List<Catalog>();
            foreach (Catalog caux in context.catalogs)
            {
                cs.Add(caux);
            }

            var result = await controller.DeleteCatalog((long)cs.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteCatalog((long)1000);
            Assert.IsType<NotFoundResult>(result2);

        }
    }
}
