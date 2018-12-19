using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controller;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;
using Xunit;

namespace SiC.Test
{
    public class FinishControllerTest
    {

        PersistenceContext context;
        FinishController controller;

        public FinishControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Finish> finishes = new List<Finish>();

            foreach (Finish f in context.finishes)
            {
                finishes.Add(f);
            }

            if (finishes.Count == 0)
            {

                Finish finish = new Finish();
                finish.Name = "finish1";
                finish.Description = "description";

                context.finishes.Add(finish);
                context.SaveChanges();

            }

            controller = new FinishController(context);
        }

        [Fact]
        public async void GetFinishById()
        {
            //Should find
            var result = await controller.GetFinishById((long)1);
            Assert.IsType<OkObjectResult>(result);

            //Should not find
            var result2 = await controller.GetFinishById((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void TestPutFinish()
        {
            FinishDTO dto = new FinishDTO();
            dto.Name = "Name";
            dto.Description = "Description";

            var result = await controller.PutFinish((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutFinish((long)1, null);
            Assert.IsType<BadRequestResult>(result2);

            var result3 = await controller.PutFinish((long)1000, dto);
            Assert.IsType<NotFoundResult>(result3);
        }

        [Fact]
        public async void TestPostFinish()
        {
            FinishDTO dto = new FinishDTO();
            dto.Name = "Name";
            dto.Description = "Description";

            var result = await controller.PostFinish(dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PostFinish(null);
            Assert.IsType<BadRequestResult>(result2);

            FinishDTO dtoNull = new FinishDTO();
            var result3 = await controller.PostFinish(dtoNull);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async void DeleteFinishTest()
        {
            FinishDTO f = new FinishDTO();
            f.Name = "Name";
            f.Description = "Description";
            await controller.PostFinish(f);

            List<Finish> fs = new List<Finish>();
            foreach (Finish faux in context.finishes)
            {
                fs.Add(faux);
            }

            var result = await controller.DeleteFinish((long)fs.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteFinish((long)1000);
            Assert.IsType<NotFoundResult>(result2);

        }

    }
}
