using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.Controllers;
using SiC.DTOs;
using SiC.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LAPR5_GC.UnitTests
{
    public class CatalogControllerTest
    {
        SiCContext context;
        CatalogController controller;
        public CatalogControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<SiCContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new SiCContext(options);

            List<Catalog> catalogs = new List<Catalog>();

            foreach (Catalog c in context.Catalog)
            {
                catalogs.Add(c);
            }

            if (catalogs.Count == 0)
            {
                Catalog mock1 = new Catalog();
                mock1.Date = "06/01/2019";
                mock1.CatalogDescription = "This is a mock Catalog";
                mock1.CatalogName = "Test_Catalog";
                mock1.CatalogProducts = new List<CatalogProduct>();

                Catalog mock2 = new Catalog();
                mock2.Date = "06/01/2019";
                mock2.CatalogDescription = "This is another mock Catalog";
                mock2.CatalogName = "Second_Test_Catalog";
                mock2.CatalogProducts = new List<CatalogProduct>();

                Category mock3 = new Category();
                mock3.description = "This is a mock Category";
                mock3.name = "Test_Category";
                mock3.parent = null;

                Product mock4 = new Product();
                mock4.CatalogProducts = new List<CatalogProduct>();
                mock4.CollectionProducts = new List<CollectionProduct>();
                mock4.ProductMaterials = new List<ProductMaterial>();
                mock4.description = "This is a mock Product";
                mock4.dimensions = new List<Dimension>();
                mock4.name = "Test_Product";

                context.Catalog.Add(mock1);
                context.Catalog.Add(mock2);
                context.Category.Add(mock3);
                context.Product.Add(mock4);
                context.SaveChanges();
            }
            controller = new CatalogController(context);
        }

        [Fact]
        public void TestGetCatalogsSuccess()
        {
            //Should find 2 Catalogs
            List<CatalogDTO> result = (List<CatalogDTO>)controller.GetCatalog();
            Assert.True(2 == result.Count);
        }

        [Fact]
        public void TestGetCatalogsFail()
        {
            //Should not find 1 Catalog
            List<CatalogDTO> result = (List<CatalogDTO>)controller.GetCatalog();
            Assert.False(1 == result.Count);
        }

        [Fact]
        public async Task TestGetCatalogSuccess()
        {
            //Should find
            var result = await controller.GetCatalog(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetCatalogFail()
        {
            //Should not find
            var result2 = await controller.GetCatalog(1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestPutCatalogSuccess()
        {
            //Should update Catalog
            CatalogDTO dto = new CatalogDTO();
            dto.Date = "07/01/2019";
            dto.CatalogDescription = "This is a updated mock Catalog";
            dto.CatalogName = "Test_Catalog_Updated";
            dto.CatalogId = 1;
            dto.products = new List<ProductDTO>();

            var result = await controller.PutCatalog(1, dto);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCatalog(1);

            CatalogDTO new_dto = (CatalogDTO)new_res.Value;

            //Information inside the retrived CatalogDTO should be updated 
            Assert.Equal("Test_Catalog_Updated", new_dto.CatalogName);
            Assert.Equal("This is a updated mock Catalog", new_dto.CatalogDescription);
            Assert.Equal("07/01/2019", new_dto.Date);
            Assert.True(1 == new_dto.CatalogId);
        }

        [Fact]
        public async Task TestPutCatalogFail()
        {
            CatalogDTO dto = new CatalogDTO();
            dto.Date = "07/01/2019";
            dto.CatalogDescription = "This is a updated mock Catalog";
            dto.CatalogName = "Test_Catalog_Updated";
            dto.CatalogId = 1000;
            dto.products = new List<ProductDTO>();

            //Should not update if the catalog does not exist
            var result = await controller.PutCatalog(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a mismatch between a route id and catalogId
            dto.CatalogId = 1;

            var result1 = await controller.PutCatalog(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a catalog with the updated name already
            dto.CatalogName = "Second_Test_Catalog";

            var result2 = await controller.PutCatalog(1, dto);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestPostCatalogSuccess()
        {
            CatalogDTO dto = new CatalogDTO();
            dto.Date = "06/01/2019";
            dto.CatalogDescription = "This is a mock Catalog to test the post";
            dto.CatalogName = "Post_Test_Catalog";
            dto.products = new List<ProductDTO>();

            //Should create the new catalog
            var result = await controller.PostCatalog(dto);
            Assert.IsType<CreatedAtActionResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCatalog(3);

            CatalogDTO new_dto = (CatalogDTO)new_res.Value;

            //Information inside the retrived CatalogDTO should be equivalent to the newly created catalog 
            Assert.Equal("Post_Test_Catalog", new_dto.CatalogName);
            Assert.Equal("This is a mock Catalog to test the post", new_dto.CatalogDescription);
            Assert.Equal("06/01/2019", new_dto.Date);
            Assert.True(3 == new_dto.CatalogId);
        }

        [Fact]
        public async Task TestPostCatalogFail()
        {
            CatalogDTO dto = new CatalogDTO();
            dto.Date = "06/01/2019";
            dto.CatalogDescription = "This is a mock Catalog to test the post";
            dto.CatalogName = "Test_Catalog";
            dto.products = new List<ProductDTO>();

            //Should not create the new catalog if the name already exists
            var result = await controller.PostCatalog(dto);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestDeleteCatalogSuccess()
        {
            //Should remove the catalog
            var result = await controller.DeleteCatalog(2);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)result;

            CatalogDTO new_dto = (CatalogDTO)new_res.Value;

            //Information inside the elimanated CatalogDTO should be equivalent to the requested catalog for deletion 
            Assert.Equal("Second_Test_Catalog", new_dto.CatalogName);
            Assert.Equal("This is another mock Catalog", new_dto.CatalogDescription);
            Assert.Equal("06/01/2019", new_dto.Date);
            Assert.True(2 == new_dto.CatalogId);

            //Should not be possible to get the catalog once its deleted
            var result2 = await controller.GetCatalog(2);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestDeleteCatalogFail()
        {
            //Should not remove the catalog if it does not exist
            var result = await controller.DeleteCatalog(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestAddProductSuccess()
        {
            //Should update Catalog by adding a product to its list
            var result = await controller.AddProduct(1, 1);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCatalog(1);

            CatalogDTO new_dto = (CatalogDTO)new_res.Value;

            //Information inside the retrived CatalogDTO should be updated 
            Assert.Equal("Test_Catalog", new_dto.CatalogName);
            Assert.Equal("This is a mock Catalog", new_dto.CatalogDescription);
            Assert.Equal("06/01/2019", new_dto.Date);
            Assert.True(1 == new_dto.CatalogId);
            Assert.True(1 == new_dto.products.Count);

            //The product inside the list should the intended
            ProductDTO pdto = new_dto.products.Find(p => p.name == "Test_Product");

            Assert.Equal("Test_Product", pdto.name);
            Assert.Equal("This is a mock Product", pdto.description);
            Assert.True(1 == pdto.ProductId);
        }

        [Fact]
        public async Task TestAddProductFail()
        {
            //Should not update Catalog if the catalog does not exist
            var result = await controller.AddProduct(1000, 1);
            Assert.IsType<BadRequestResult>(result);

            //Should not update Catalog if the product does not exist
            var result1 = await controller.AddProduct(1, 1000);
            Assert.IsType<BadRequestResult>(result1);

            //Should not update Catalog if the product is already in the list
            var result2 = await controller.AddProduct(1, 1);
            result2 = await controller.AddProduct(1, 1);
            Assert.IsType<BadRequestResult>(result1);

        }

        [Fact]
        public async Task TestDeleteProductSuccess()
        {
            //Should update Catalog by deleting a product from its list
            var result = await controller.AddProduct(1, 1);
            result = await controller.DeleteProduct(1, 1);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCatalog(1);

            CatalogDTO new_dto = (CatalogDTO)new_res.Value;

            //Information inside the retrived CatalogDTO should be updated 
            Assert.Equal("Test_Catalog", new_dto.CatalogName);
            Assert.Equal("This is a mock Catalog", new_dto.CatalogDescription);
            Assert.Equal("06/01/2019", new_dto.Date);
            Assert.True(1 == new_dto.CatalogId);
            Assert.True(0 == new_dto.products.Count);
        }

        [Fact]
        public async Task TestDeleteProductFail()
        {
            //Should not update Catalog if catalog does not exist
            var result = await controller.DeleteProduct(1000, 1);
            Assert.IsType<NotFoundResult>(result);

            //Should not update Catalog if product does not exist
            var result1 = await controller.DeleteProduct(1, 1000);
            Assert.IsType<NotFoundResult>(result1);

            //Should not update Catalog if product does not exist in the list
            var result2 = await controller.DeleteProduct(1, 1);
            Assert.IsType<NotFoundResult>(result2);
        }

    }
}