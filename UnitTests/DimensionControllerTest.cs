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
    public class DimensionControllerTest
    {

        PersistenceContext context;
        DimensionController controller;


        public DimensionControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);
            controller = new DimensionController(context);

            List<Dimension> dimensions = new List<Dimension>();

            foreach (Dimension d in context.dimensions)
            {
                dimensions.Add(d);
            }

            if (dimensions.Count == 0)
            {
                Dimension dimension = new Dimension();

                var height = new Measure(100);
                var width = new Measure(100);
                var depth = new Measure(100);

                dimension.Height = height;
                dimension.Width = width;
                dimension.Depth = depth;

                context.measures.Add(height);
                context.measures.Add(width);
                context.measures.Add(depth);
                context.dimensions.Add(dimension);
                context.SaveChanges();
            }
        }

                [Fact]
        public async void PostDimensionTest()
        {
            DimensionDTO dto = new DimensionDTO();
            dto.Width = 150;
            dto.Height = 100;
            dto.Depth = 150;

            DimensionDTO dto2 = new DimensionDTO();
            dto2.Width = 0;
            dto2.Height = 0;
            dto2.Depth = 0;

            //Should add normally
            var result = await controller.PostDimension(dto);
            Assert.IsType<OkObjectResult>(result);

            //Can't post a null object
            var result2 = await controller.PostDimension(null);
            Assert.IsType<BadRequestResult>(result2);

            //Can't add a dimension without valid measurements
            var result3 = await controller.PostDimension(dto2);
            Assert.IsType<BadRequestResult>(result3);

        }

        [Fact]
        public async void GetDimensionByIdTest()
        {
            //ID exists
            var result = await controller.GetDimensionById((long)1);
            Assert.IsType<OkObjectResult>(result);

            //Id does not exist
            var result2 = await controller.GetDimensionById((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void PutDimensionTest()
        {
            DimensionDTO dto = new DimensionDTO();
            dto.Width = 150;
            dto.Height = 100;
            dto.Depth = 150;

            //Should work
            var result = await controller.PutDimension((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            //Id does not exist
            var result1 = await controller.PutDimension((long)1000, null);
            Assert.IsType<BadRequestObjectResult>(result1);

            //Id exists but dto is null
            var result2 = await controller.PutDimension((long)1, null);
            Assert.IsType<BadRequestObjectResult>(result2);

            //Id does not exist but dto is correct
            var result3 = await controller.PutDimension((long)1000, dto);
            Assert.IsType<NotFoundResult>(result3);

        }

        [Fact]
        public async void DeleteDimensionTest()
        {
            DimensionDTO d = new DimensionDTO();
            d.Width = 150;
            d.Height = 150;
            d.Depth = 150;

            await controller.PostDimension(d);

            List<Dimension> ds = new List<Dimension>();
            foreach (Dimension daux in context.dimensions)
            {
                ds.Add(daux);
            }

            var result = await controller.DeleteDimension((long)ds.Count-1);
            Assert.IsType<OkObjectResult>(result);
            var result2 = await controller.DeleteDimension((long)100980);
            Assert.IsType<NotFoundResult>(result2);

        }

    }
}
