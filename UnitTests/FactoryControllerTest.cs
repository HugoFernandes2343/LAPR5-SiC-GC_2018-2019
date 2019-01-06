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
    public class FactoryControllerTest
    {
        SiCContext context;
        FactoryController controller;
        public FactoryControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<SiCContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new SiCContext(options);

            List<Factory> factories = new List<Factory>();

            foreach (Factory f in context.Factory)
            {
                factories.Add(f);
            }

            if (factories.Count == 0)
            {
                City mock1 = new City("Test_City", 40.0, -8.0);
                City mock2 = new City("Second_Test_City", -40.0, 8.0);
                City mock3 = new City("Third_Test_City", -20.0, 4.0);


                Factory mock4 = new Factory();
                mock4.City = mock1;
                mock4.Description= "Test_Factory";

                Factory mock5 = new Factory();
                mock5.City = mock2;
                mock5.Description= "Second_Test_Factory";

                context.City.Add(mock1);
                context.City.Add(mock2);
                context.City.Add(mock3);
                context.Factory.Add(mock4);
                context.Factory.Add(mock5);
                context.SaveChanges();
            }
            controller = new FactoryController(context);
        }

        [Fact]
        public void TestGetFactoriesSuccess()
        {
            //Should find 2 Factories
            List<FactoryDTO> result = (List<FactoryDTO>)controller.GetFactory();
            Assert.True(2 == result.Count);
        }

        [Fact]
        public void TestGetFactoriesFail()
        {
            //Should not find 1 Factory
            List<FactoryDTO> result = (List<FactoryDTO>)controller.GetFactory();
            Assert.False(1 == result.Count);
        }

        [Fact]
        public async Task TestGetFactorySuccess()
        {
            //Should find
            var result = await controller.GetFactory(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetFactoryFail()
        {
            //Should not find
            var result = await controller.GetFactory(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestPutFactorySuccess()
        {
            //Should update Factory
            FactoryDTO dto = new FactoryDTO();
            dto.Description = "Test_Factory_Updated";
            dto.FactoryId = 1;

            var result = await controller.PutFactory(1, dto);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetFactory(1);

            FactoryDTO new_dto = (FactoryDTO)new_res.Value;

            //Information inside the retrived FactoryDTO should be updated 
            Assert.Equal("Test_Factory_Updated", new_dto.Description);
            Assert.True(1 == new_dto.FactoryId);
        }

        [Fact]
        public async Task TestPutFactoryFail()
        {
            FactoryDTO dto = new FactoryDTO();
            dto.Description = "Test_Factory";
            dto.FactoryId = 1000;

            //Should not update if the factory does not exist
            var result = await controller.PutFactory(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a mismatch between a route id and FactoryId
            dto.FactoryId = 1;

            var result1 = await controller.PutFactory(1000, dto);
            Assert.IsType<BadRequestResult>(result1);

            //Should not update if the factory description already exists
            var result2 = await controller.PutFactory(1, dto);
            Assert.IsType<BadRequestResult>(result2);
        }

        [Fact]
        public async Task TestPostFactorySuccess()
        {
            CityDTO cdto = new CityDTO();
            cdto.Latitude = -20.0;
            cdto.Longitude = 4.0;
            cdto.CityId = 3;
            cdto.Name = "Third_Test_City";

            FactoryDTO dto = new FactoryDTO();
            dto.Description = "Post_Test_Factory";
            dto.City = cdto;


            //Should create the new factory
            var result = await controller.PostFactory(dto);
            Assert.IsType<CreatedAtActionResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetFactory(3);

            FactoryDTO new_dto = (FactoryDTO)new_res.Value;

            //Information inside the retrived FactoryDTO should be equivalent to the newly created factory
            Assert.Equal("Post_Test_Factory", new_dto.Description);
            Assert.True(3 == new_dto.FactoryId);
            Assert.Equal("Third_Test_City", new_dto.City.Name);
            Assert.True(-20.0 == new_dto.City.Latitude);
            Assert.True(4.0 == new_dto.City.Longitude);
            Assert.True(3 == new_dto.City.CityId);
        }

        [Fact]
        public async Task TestPostFactoryFail()
        {
            CityDTO cdto = new CityDTO();
            cdto.Latitude = -40.0;
            cdto.Longitude = 8.0;
            cdto.CityId = 2;
            cdto.Name = "Second_Test_City";

            FactoryDTO dto = new FactoryDTO();
            dto.Description = "Test_Factory";
            dto.City = cdto;

            //Should not create the new factory if the name already exists
            var result = await controller.PostFactory(dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not create the new factory if the city is already used
            dto.Description = "Post_Test_City";
            var result1 = await controller.PostFactory(dto);
            Assert.IsType<BadRequestResult>(result1);

            //Should not create the new factory if the city does not exists
            dto.City.CityId = 1000;
            dto.City.Name = "Post_Test_City";
            var result2 = await controller.PostFactory(dto);
            Assert.IsType<BadRequestResult>(result2);
        }

        [Fact]
        public async Task TestDeleteFactorySuccess()
        {
            //Should remove the factory
            var result = await controller.DeleteFactory(2);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)result;

            FactoryDTO new_dto = (FactoryDTO)new_res.Value;

            //Information inside the elimanated FactoryDTO should be equivalent to the requested factory for deletion 
            Assert.Equal("Second_Test_Factory", new_dto.Description);
            Assert.True(2 == new_dto.FactoryId);

            //Should not be possible to get the factory once its deleted
            var result2 = await controller.GetFactory(2);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestDeleteFactoryFail()
        {
            //Should not remove the factory if it does not exist
            var result = await controller.DeleteFactory(1000);
            Assert.IsType<NotFoundResult>(result);
        }

    }
}