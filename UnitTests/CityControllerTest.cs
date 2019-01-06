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
    public class CityControllerTest
    {

        SiCContext context;
        CityController controller;
        public CityControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<SiCContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new SiCContext(options);

            List<City> cities = new List<City>();

            foreach (City c in context.City)
            {
                cities.Add(c);
            }

            if (cities.Count == 0)
            {
                City mock1 = new City("Test_City", 40.0, -8.0);
                City mock2 = new City("Second_Test_City", -40.0, 8.0);

                context.City.Add(mock1);
                context.City.Add(mock2);
                context.SaveChanges();
            }
            controller = new CityController(context);
        }

        [Fact]
        public void TestGetCitiesSuccess()
        {
            //Should find 2 Cities
            List<CityDTO> result = (List<CityDTO>)controller.GetCity();
            Assert.True(2 == result.Count);
        }

        [Fact]
        public void TestGetCitiesFail()
        {
            //Should not find 1 City
            List<CityDTO> result = (List<CityDTO>)controller.GetCity();
            Assert.False(1 == result.Count);
        }

        [Fact]
        public async Task TestGetCitySuccess()
        {
            //Should find
            var result = await controller.GetCity(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetCityFail()
        {
            //Should not find
            var result = await controller.GetCity(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestPutCitySuccess()
        {
            //Should update City
            CityDTO dto = new CityDTO();
            dto.CityId = 1;
            dto.Latitude = 30.0;
            dto.Longitude = 30.0;
            dto.Name = "Test_City_Updated";

            var result = await controller.PutCity(1, dto);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCity(1);

            CityDTO new_dto = (CityDTO)new_res.Value;

            //Information inside the retrived CityDTO should be updated 
            Assert.Equal("Test_City_Updated", new_dto.Name);
            Assert.True(30.0 == new_dto.Latitude);
            Assert.True(30.0 == new_dto.Longitude);
            Assert.True(1 == new_dto.CityId);
        }

        [Fact]
        public async Task TestPutCityFail()
        {
            CityDTO dto = new CityDTO();
            dto.CityId = 1000;
            dto.Latitude = -40.0;
            dto.Longitude = 8.0;
            dto.Name = "Second_Test_City";

            //Should not update if the city does not exist
            var result = await controller.PutCity(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a mismatch between a route id and CityId
            dto.CityId = 1;

            var result1 = await controller.PutCity(1000, dto);
            Assert.IsType<BadRequestResult>(result1);

            //Should not update if the city name already exists
            var result2 = await controller.PutCity(1, dto);
            Assert.IsType<BadRequestResult>(result2);

            //Should not update if the combination latitude/longitude already exists
            dto.Name = "Test_City_Update";
            var result3 = await controller.PutCity(1, dto);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async Task TestPostCitySuccess()
        {
            CityDTO dto = new CityDTO();
            dto.Latitude = -20.0;
            dto.Longitude = 4.0;
            dto.Name = "Post_Test_City";


            //Should create the new city
            var result = await controller.PostCity(dto);
            Assert.IsType<CreatedAtActionResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetCity(3);

            CityDTO new_dto = (CityDTO)new_res.Value;

            //Information inside the retrived CityDTO should be equivalent to the newly created city
            Assert.Equal("Post_Test_City", new_dto.Name);
            Assert.True(-20.0 == new_dto.Latitude);
            Assert.True(4.0 == new_dto.Longitude);
            Assert.True(3 == new_dto.CityId);
        }

        [Fact]
        public async Task TestPostCityFail()
        {
            CityDTO dto = new CityDTO();
            dto.Latitude = -40.0;
            dto.Longitude = 8.0;
            dto.Name = "Second_Test_City";

            //Should not create the new city if the name already exists
            var result = await controller.PostCity(dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not create the new city if the combination latitude/longitude already exists
            dto.Name = "Post_Test_City";
            var result1 = await controller.PostCity(dto);
            Assert.IsType<BadRequestResult>(result1);
        }

        [Fact]
        public async Task TestDeleteCitySuccess()
        {
            //Should remove the city
            var result = await controller.DeleteCity(2);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)result;

            CityDTO new_dto = (CityDTO)new_res.Value;

            //Information inside the elimanated CityDTO should be equivalent to the requested city for deletion 
            Assert.Equal("Second_Test_City", new_dto.Name);
            Assert.True(-40.0 == new_dto.Latitude);
            Assert.True(8.0 == new_dto.Longitude);
            Assert.True(2 == new_dto.CityId);

            //Should not be possible to get the city once its deleted
            var result2 = await controller.GetCity(2);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestDeleteCityFail()
        {
            //Should not remove the city if it does not exist
            var result = await controller.DeleteCity(1000);
            Assert.IsType<BadRequestResult>(result);
        }

    }
}