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
    public class UserControllerTest
    {
        PersistenceContext context;
        UserController controller;

        public UserControllerTest()
        {
            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);

            List<User> users = new List<User>();

            foreach (User user in context.users)
            {
                users.Add(user);
            }

            if (users.Count == 0)
            {
                User user = new User();
                user.Email = "email@email.com";
                user.FirstName = "Joao";
                context.users.Add(user);
                context.SaveChanges();
            }

            controller = new UserController(context);
        }

        [Fact]
        public void TestGetAllUsers()
        {
            var result = controller.GetAll();
            Assert.IsType<List<GetUserDTO>>(result);
        }

        [Fact]
        public async void TestGetByName()
        {
            var result = await controller.GetByName("Joao");
            Assert.IsType<OkObjectResult>(result);
            var result2 = await controller.GetByName("NameQueNaoExiste");
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void TestPutUser()
        {
            UserDTO dto = new UserDTO();
            dto.Email = "email@email.com";
            dto.FirstName = "Joao";
            dto.LastName = "Mano";
            dto.Password = "asdfasdfasdf";

            var result = await controller.PutUser(dto);
            Assert.IsType<OkResult>(result);

            var result2 = await controller.PutUser(null);
            Assert.IsType<BadRequestResult>(result2);

            UserDTO dto2 = new UserDTO();
            dto.Email = "email2@email.com";
            dto.FirstName = "Joao";
            dto.LastName = "Mano";
            dto.Password = "asdfasdfasdf";

            var result3 = await controller.PutUser(dto);
            Assert.IsType<NotFoundResult>(result3);
        }

        [Fact]
        public async Task TestDeleteUser()
        {
            UserDTO dto = new UserDTO();
            UserDTO dto2 = new UserDTO();

            string email = "email@email.com";
            string wrongEmail = "adsa";

            dto.Email = email;
            dto2.Email = wrongEmail;

            var result = await controller.DeleteUser(dto);
            var result2 = await controller.DeleteUser(dto2);

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<NotFoundResult>(result2);
        }

    }
}
