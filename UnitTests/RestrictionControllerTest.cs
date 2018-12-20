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
    public class RestrictionControllertest
    {

        PersistenceContext context;
        RestrictionController controller;

        public RestrictionControllertest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Restriction> restrictions = new List<Restriction>();

            foreach (Restriction r in context.restrictions)
            {
                restrictions.Add(r);
            }

            if (restrictions.Count == 0)
            {

                Restriction restriction = new Restriction();
                context.restrictions.Add(restriction);
                context.SaveChanges();

            }
            controller = new RestrictionController(context);
        }

        [Fact]
        public async void TestPostRestriction()
        {
            RestrictionDTO dto = new RestrictionDTO();

            var result = await controller.PostRestriction(dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PostRestriction(null);
            Assert.IsType<BadRequestResult>(result2);
        }

                [Fact]
        public async void TestPutRestriction()
        {
            RestrictionDTO dto = new RestrictionDTO();

            var result = await controller.PutRestriction((long)1, dto);
            Assert.IsType<OkResult>(result);

            var result2 = await controller.PutRestriction((long)1, null);
            Assert.IsType<BadRequestResult>(result2);
        }

        [Fact]
        public async void TestDeleteRestriction()
        {

            RestrictionDTO dto = new RestrictionDTO();
            await controller.PostRestriction(dto);

            List<Restriction> rs = new List<Restriction>();
            foreach (Restriction raux in context.restrictions)
            {
                rs.Add(raux);
            }

            var result = await controller.DeleteRestriction((long)rs.Count-1);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteRestriction((long)1000);
            Assert.IsType<NotFoundResult>(result2);

        }
    }
}
