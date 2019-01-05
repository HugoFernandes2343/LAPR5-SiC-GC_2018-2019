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
    public class CollectionControllerTest
    {

        SiCContext context;
        CollectionController controller;
        public CollectionControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<SiCContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new SiCContext(options);

            List<Collection> collections = new List<Collection>();

            foreach (Collection c in context.Collection)
            {
                collections.Add(c);
            }

            if (collections.Count == 0)
            {
                Collection mock1 = new Collection();
                mock1.collectionName = "Test_Collection";
                mock1.aestheticParameter = "Mock_Parameter";
                mock1.CollectionProducts = new List<CollectionProduct>();

                Collection mock2 = new Collection();
                mock2.collectionName = "Second_Test_Collection";
                mock2.aestheticParameter = "Second_Mock_Parameter";
                mock2.CollectionProducts = new List<CollectionProduct>();

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

                context.Collection.Add(mock1);
                context.Collection.Add(mock2);
                context.Category.Add(mock3);
                context.Product.Add(mock4);
                context.SaveChanges();
            }
            controller = new CollectionController(context);
        }

        [Fact]
        public void TestGetCollectionsSuccess()
        {
            //Should find 2 Collections
            List<CollectionDTO> result = (List<CollectionDTO>)controller.GetCollection();
            Assert.True(2 == result.Count);
        }

        [Fact]
        public void TestGetCollectionsFail()
        {
            //Should not find 1 Collection
            List<CollectionDTO> result = (List<CollectionDTO>)controller.GetCollection();
            Assert.False(1 == result.Count);
        }

        [Fact]
        public async Task TestGetCollectionSuccess()
        {
            //Should find
            var result = await controller.GetCollection(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetCollectionFail()
        {
            //Should not find
            var result = await controller.GetCollection(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestPutCollectionSuccess()
        {
            //Should update Collection
            CollectionDTO dto = new CollectionDTO();
            dto.CollectionId = 1;
            dto.collectionName = "Test_Collection_Updated";
            dto.aestheticParameter = "Second_Mock_Parameter_Updated";
            dto.products = new List<ProductDTO>();

            var result = await controller.PutCollection(1, dto);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCollection(1);

            CollectionDTO new_dto = (CollectionDTO)new_res.Value;

            //Information inside the retrived CollectionDTO should be updated 
            Assert.Equal("Test_Collection_Updated", new_dto.collectionName);
            Assert.Equal("Second_Mock_Parameter_Updated", new_dto.aestheticParameter);
            Assert.True(1 == new_dto.CollectionId);
        }

        [Fact]
        public async Task TestPutCollectionFail()
        {
            CollectionDTO dto = new CollectionDTO();
            dto.CollectionId = 1000;
            dto.collectionName = "Test_Collection_Updated";
            dto.aestheticParameter = "Second_Mock_Parameter_Updated";
            dto.products = new List<ProductDTO>();

            //Should not update if the collection does not exist
            var result = await controller.PutCollection(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a mismatch between a route id and CollectionId
            dto.CollectionId = 1;

            var result1 = await controller.PutCollection(1000, dto);
            Assert.IsType<BadRequestResult>(result1);

            //Should not update if there is a collection with the updated name already
            dto.collectionName = "Second_Test_Collection";

            var result2 = await controller.PutCollection(1, dto);
            Assert.IsType<BadRequestResult>(result2);
        }

        [Fact]
        public async Task TestPostCollectionSuccess()
        {
            CollectionDTO dto = new CollectionDTO();
            dto.collectionName = "Post_Test_Collection";
            dto.aestheticParameter = "Post_Mock_Parameter";
            dto.products = new List<ProductDTO>();

            //Should create the new collection
            var result = await controller.PostCollection(dto);
            Assert.IsType<CreatedAtActionResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCollection(3);

            CollectionDTO new_dto = (CollectionDTO)new_res.Value;

            //Information inside the retrived CollectionDTO should be equivalent to the newly created collection
            Assert.Equal("Post_Test_Collection", new_dto.collectionName);
            Assert.Equal("Post_Mock_Parameter", new_dto.aestheticParameter);
            Assert.True(3 == new_dto.CollectionId);
        }

       [Fact]
        public async Task TestPostCollectionFail()
        {
            CollectionDTO dto = new CollectionDTO();
            dto.collectionName = "Test_Collection";
            dto.aestheticParameter = "Post_Mock_Parameter";
            dto.products = new List<ProductDTO>();

            //Should not create the new collection if the name already exists
            var result = await controller.PostCollection(dto);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestDeleteCollectionSuccess()
        {
            //Should remove the collection
            var result = await controller.DeleteCollection(2);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)result;

            CollectionDTO new_dto = (CollectionDTO)new_res.Value;

            //Information inside the elimanated CollectionDTO should be equivalent to the requested collection for deletion 
            Assert.Equal("Second_Test_Collection", new_dto.collectionName);
            Assert.Equal("Second_Mock_Parameter", new_dto.aestheticParameter);
            Assert.True(2 == new_dto.CollectionId);

            //Should not be possible to get the collection once its deleted
            var result2 = await controller.GetCollection(2);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestDeleteCollectionFail()
        {
            //Should not remove the collection if it does not exist
            var result = await controller.DeleteCollection(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestAddProductSuccess()
        {
            //Should update Collection by adding a product to its list
            var result = await controller.AddProduct(1, 1);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCollection(1);

            CollectionDTO new_dto = (CollectionDTO)new_res.Value;

            //Information inside the retrived CollectionDTO should be updated 
            Assert.Equal("Test_Collection", new_dto.collectionName);
            Assert.Equal("Mock_Parameter", new_dto.aestheticParameter);
            Assert.True(1 == new_dto.CollectionId);
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
            //Should not update Collection if the collection does not exist
            var result = await controller.AddProduct(1000, 1);
            Assert.IsType<BadRequestResult>(result);

            //Should not update Collection if the product does not exist
            var result1 = await controller.AddProduct(1, 1000);
            Assert.IsType<BadRequestResult>(result1);

            //Should not update Collection if the product is already in the list
            var result2 = await controller.AddProduct(1, 1);
            result2 = await controller.AddProduct(1, 1);
            Assert.IsType<BadRequestResult>(result2);

        }

        [Fact]
        public async Task TestDeleteProductSuccess()
        {
            //Should update Collection by deleting a product from its list
            var result = await controller.AddProduct(1, 1);
            result = await controller.DeleteProduct(1, 1);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCollection(1);

            CollectionDTO new_dto = (CollectionDTO)new_res.Value;
            
            //Information inside the retrived CollectionDTO should be updated 
            Assert.Equal("Test_Collection", new_dto.collectionName);
            Assert.Equal("Mock_Parameter", new_dto.aestheticParameter);
            Assert.True(1 == new_dto.CollectionId);
            Assert.True(0 == new_dto.products.Count);
        }

        [Fact]
        public async Task TestDeleteProductFail()
        {
            //Should not update Collection if collection does not exist
            var result = await controller.DeleteProduct(1000, 1);
            Assert.IsType<NotFoundResult>(result);

            //Should not update Collection if product does not exist
            var result1 = await controller.DeleteProduct(1, 1000);
            Assert.IsType<NotFoundResult>(result1);

            //Should not update Collection if product does not exist in the list
            var result2 = await controller.DeleteProduct(1, 1);
            Assert.IsType<NotFoundResult>(result2);
        }

    }
}