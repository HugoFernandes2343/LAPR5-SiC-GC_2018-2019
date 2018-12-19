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
    public class MaterialControllerTest
    {

        PersistenceContext context;
        MaterialController controller;

        public MaterialControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Material> materials = new List<Material>();

            foreach (Material m in context.materials)
            {
                materials.Add(m);
            }

            if (materials.Count == 0)
            {

                Finish finish = new Finish();
                finish.Name = "finish1";
                finish.Description = "description";

                context.finishes.Add(finish);

                Material material = new Material();
                material.Name = "Nome";
                material.Description = "Desc";

                context.materials.Add(material);

                context.SaveChanges();

            }

            controller = new MaterialController(context);
        }

        [Fact]
        public async void GetMaterialById()
        {
            //Should find
            var result = await controller.GetMaterialById((long)1);
            Assert.IsType<OkObjectResult>(result);

            //Should not find
            var result2 = await controller.GetMaterialById((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void TestPutMaterial()
        {
            MaterialDTO dto = new MaterialDTO();
            dto.Name = "Name";
            dto.Description = "Description";

            var result = await controller.PutMaterial((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutMaterial((long)1, null);
            Assert.IsType<BadRequestResult>(result2);

            var result3 = await controller.PutMaterial((long)1000, dto);
            Assert.IsType<NotFoundResult>(result3);
        }

        [Fact]
        public async void TestPostMaterial()
        {
            MaterialDTO dto = new MaterialDTO();
            dto.Name = "Name";
            dto.Description = "Description";

            var result = await controller.PostMaterial(dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PostMaterial(null);
            Assert.IsType<BadRequestResult>(result2);

            MaterialDTO dtoNull = new MaterialDTO();
            var result3 = await controller.PostMaterial(dtoNull);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async void DeleteMaterialTest()
        {
            MaterialDTO dto = new MaterialDTO();
            dto.Name = "Name";
            dto.Description = "Description";
            await controller.PostMaterial(dto);

            List<Material> ms = new List<Material>();
            foreach (Material maux in context.materials)
            {
                ms.Add(maux);
            }

            var result = await controller.DeleteMaterial((long)ms.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteMaterial((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }
 

    }
}
