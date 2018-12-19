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
    public class CategoryControllerTest
    {

        PersistenceContext context;
        CategoryController controller;


        public CategoryControllerTest()
        {

            var connection = "TESTDB";
            var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseInMemoryDatabase(connection)
            .Options;

            context = new PersistenceContext(options);
            controller = new CategoryController(context);

            List<Category> categories = new List<Category>();

            foreach (Category c in context.categories)
            {
                categories.Add(c);
            }

            if (categories.Count == 0)
            {

                Category category = new Category();

                category.Name = "Wardrobe";
                context.categories.Add(category);
                context.SaveChanges();

                Category category2 = new Category();

                category2.Name = "Wardrobe Child";
                category2.ParentID = 1;
                context.categories.Add(category2);

                context.SaveChanges();

            }
        }

        [Fact]
        public async void PutCategoryTest()
        {
            PostCategoryDTO dto = new PostCategoryDTO();
            dto.Name = "Name";
            dto.ParentID = -1;

            var result = await controller.PutCategory((long)1, dto);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.PutCategory((long)1000, dto);
            Assert.IsType<NotFoundResult>(result2);

            var result3 = await controller.PutCategory((long)1, null);
            Assert.IsType<BadRequestResult>(result3);
        }

        [Fact]
        public async void PostCategoryTest()
        {
            PostCategoryDTO dto = new PostCategoryDTO();
            dto.Name = "Name";

            //Should work
            var result = await controller.PostCategory(dto);
            Assert.IsType<OkObjectResult>(result);

            //If name is null
            PostCategoryDTO dtoNull = new PostCategoryDTO();
            var result2 = await controller.PostCategory(dtoNull);
            Assert.IsType<BadRequestResult>(result2);

            var result3 = await controller.PostCategory(null);
            Assert.IsType<BadRequestResult>(result3);
        }


        [Fact]
        public async void DeleteCategoryTest()
        {
            PostCategoryDTO dto = new PostCategoryDTO();
            dto.Name = "Le categoria";

            await controller.PostCategory(dto);

            List<Category> categories = new List<Category>();

            foreach(Category c in context.categories)
            {
                categories.Add(c);
            }

            var result = await controller.DeleteCategory(categories.Count);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.DeleteCategory((long) 1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void GetByIdTest()
        {
            var result = await controller.GetById((long)1);
            Assert.IsType<OkObjectResult>(result);

            var result2 = await controller.GetById((long)1000);
            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public async void GetAllTest()
        {
            var result = await controller.GetAllCategory();
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
