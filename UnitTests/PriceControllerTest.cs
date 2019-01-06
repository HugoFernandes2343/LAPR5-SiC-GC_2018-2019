using System;
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
    public class PriceControllerTest
    {

        SiCContext context;
        PriceController controller;
        public PriceControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<SiCContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new SiCContext(options);

            List<Price> prices = new List<Price>();

            foreach (Price p in context.Price)
            {
                prices.Add(p);
            }

            if (prices.Count == 0)
            {
                Price mock1 = new Price();
                mock1.date = DateTime.Parse("2019-01-06");
                mock1.designation = "Test_Designation";
                mock1.price = 25.55;

                Price mock2 = new Price();
                mock2.date = DateTime.Parse("2019-01-07");
                mock2.designation = "Second_Test_Designation";
                mock2.price = 50.0;

                Price mock3 = new Price();
                mock3.date = DateTime.Parse("2019-01-08");
                mock3.designation = "Test_Designation";
                mock3.price = 20.30;

                context.Price.Add(mock1);
                context.Price.Add(mock2);
                context.Price.Add(mock3);
                context.SaveChanges();
            }
            controller = new PriceController(context);
        }

        [Fact]
        public void TestGetPricesSuccess()
        {
            //Should find 3 Prices
            List<PriceDTO> result = (List<PriceDTO>)controller.GetPrice();
            Assert.True(3 == result.Count);
        }

        [Fact]
        public void TestGetPricesFail()
        {
            //Should not find 1 Price
            List<PriceDTO> result = (List<PriceDTO>)controller.GetPrice();
            Assert.False(1 == result.Count);
        }

        [Fact]
        public async Task TestGetPriceSuccess()
        {
            //Should find
            var result = await controller.GetPrice(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetPriceFail()
        {
            //Should not find
            var result = await controller.GetPrice(1000);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestPutPriceSuccess()
        {
            //Should update Price
            PriceDTO dto = new PriceDTO();
            dto.PriceId = 1;
            dto.date = "2019-01-10";
            dto.designation = "Test_Designation_Updated";
            dto.price = 35.55;

            var result = await controller.PutPrice(1, dto);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetPrice(1);

            PriceDTO new_dto = (PriceDTO)new_res.Value;

            //Information inside the retrived PriceDTO should be updated 
            Assert.Equal("2019-01-10", new_dto.date);
            Assert.Equal("Test_Designation_Updated", new_dto.designation);
            Assert.True(35.55 == new_dto.price);
            Assert.True(1 == new_dto.PriceId);
        }

        [Fact]
        public async Task TestPutPriceFail()
        {
            PriceDTO dto = new PriceDTO();
            dto.PriceId = 1000;
            dto.date = "2019-01-10";
            dto.designation = "Test_Designation_Updated";
            dto.price = 25.55;

            //Should not update if the price does not exist
            var result = await controller.PutPrice(1000, dto);
            Assert.IsType<BadRequestResult>(result);

            //Should not update if there is a mismatch between a route id and PriceId
            dto.PriceId = 1;

            var result1 = await controller.PutPrice(1000, dto);
            Assert.IsType<BadRequestResult>(result1);
        }

        [Fact]
        public async Task TestPostPriceSuccess()
        {
            PriceDTO dto = new PriceDTO();
            dto.date = "2019-01-10";
            dto.designation = "Post_Test_Designation";
            dto.price = 5.55;


            //Should create the new price
            var result = await controller.PostPrice(dto);
            Assert.IsType<CreatedAtActionResult>(result);

            OkObjectResult new_res = (OkObjectResult)await controller.GetPrice(4);

            PriceDTO new_dto = (PriceDTO)new_res.Value;

            //Information inside the retrived PriceDTO should be equivalent to the newly created price
            Assert.Equal("2019-01-10", new_dto.date);
            Assert.Equal("Post_Test_Designation", new_dto.designation);
            Assert.True(5.55 == new_dto.price);
            Assert.True(4 == new_dto.PriceId);
        }

        [Fact]
        public async Task TestPostPriceFail()
        {
            PriceDTO dto = new PriceDTO();
            dto.date = "2019-01-10";
            dto.designation = "Post_Test_Designation";
            dto.price = -1;

            //Should not create the new price if price is less than 0
            var result = await controller.PostPrice(dto);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestDeletePriceSuccess()
        {
            //Should remove the price
            var result = await controller.DeletePrice(2);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult new_res = (OkObjectResult)result;

            PriceDTO new_dto = (PriceDTO)new_res.Value;

            //Information inside the elimanated PriceDTO should be equivalent to the requested price for deletion 
            Assert.Equal("2019-01-07", new_dto.date);
            Assert.Equal("Second_Test_Designation", new_dto.designation);
            Assert.True(50.0 == new_dto.price);
            Assert.True(2 == new_dto.PriceId);

            //Should not be possible to get the price once its deleted
            var result2 = await controller.GetPrice(2);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async Task TestDeletePriceFail()
        {
            //Should not remove the price if it does not exist
            var result = await controller.DeletePrice(1000);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestGetPriceByEntitySuccess()
        {
            //Should find 2 Prices
            var result = await controller.GetPriceByEntity("Test_Designation");
            OkObjectResult res = (OkObjectResult) result;
            List<PriceDTO> dtos = (List<PriceDTO>) res.Value;
            Assert.True(2 == dtos.Count);
        }

        [Fact]
        public async Task TestGetPriceByEntityFail()
        {
            //Should not find 2 Prices
            var result = await controller.GetPriceByEntity("Second_Test_Designation");
            OkObjectResult res = (OkObjectResult) result;
            List<PriceDTO> dtos = (List<PriceDTO>) res.Value;
            Assert.False(2 == dtos.Count);
        }

    }
}