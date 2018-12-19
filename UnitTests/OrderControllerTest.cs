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
    public class OrderControllerTest
    {
        PersistenceContext context;
        OrderController controller;

        public OrderControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<Order> orders = new List<Order>();

            foreach (Order order in context.orders)
            {
                orders.Add(order);
            }

            List<User> users = new List<User>();

            foreach (User user in context.users)
            {
                users.Add(user);
            }

            if (users.Count == 0)
            {

                User user = new User();
                user.Email = "email@email.com";
                user.FirstName = "Name";
                user.LastName = "Last";
                user.Password = "asdf";

                context.users.Add(user);

                context.SaveChanges();

            }

            if (orders.Count == 0)
            {


                Order order = new Order();
                order.Date = "7-10-2018";
                order.UserId = 1;

                context.orders.Add(order);


                context.SaveChanges();
            }

            controller = new OrderController(context);
        }

        [Fact]
        public async void TestGetOrderById()
        {
            //Should not find
            var result2 = await controller.GetOrderById((long)1000);
            Assert.IsType<NotFoundObjectResult>(result2);

        }

        [Fact]
        public async void TestPutOrder()
        {
            OrderDTO dto = new OrderDTO();
            dto.Date = "10/10/10";
            dto.UserId = 1;
            dto.Email = "email@email.com";

            var result = await controller.PutOrder((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutOrder((long)1, null);
            Assert.IsType<BadRequestResult>(result2);

            var result3 = await controller.PutOrder((long)1000, dto);
            Assert.IsType<NotFoundResult>(result3);
        }

        [Fact]
        public async void TestPostOrder()
        {
            OrderDTO dto = new OrderDTO();
            dto.Date = "10/10/10";
            dto.UserId = 1;
            dto.Email = "email@email.com";

            var result = await controller.PostOrder(dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PostOrder(null);
            Assert.IsType<BadRequestResult>(result2);

            OrderDTO dtoNull = new OrderDTO();
            var result3 = await controller.PostOrder(dtoNull);
            Assert.IsType<BadRequestObjectResult>(result3);
        }



        [Fact]
        public async void DeleteOrderTest()
        {
            OrderDTO dto = new OrderDTO();
            dto.Date = "10/10/10";
            dto.UserId = 1;
            dto.Email = "email@email.com";
            await controller.PostOrder(dto);

            List<Order> ms = new List<Order>();
            foreach (Order maux in context.orders)
            {
                ms.Add(maux);
            }

            var result = await controller.DeleteOrder((long)ms.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteOrder((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

    }
}
