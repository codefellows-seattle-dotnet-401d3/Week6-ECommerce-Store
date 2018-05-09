using ECommerce.Controllers;
using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace ECommerceTest
{
    public class ControllerTests
    {
        [Fact]
        public void CanGetHomeIndexView()
        {
            HomeController home = new HomeController();

            IActionResult ar = home.Index();

            Assert.IsType<ViewResult>(ar);
        }

        [Fact]
        public async void CanReturnProductIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CanReturnShopIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ShopController(context);
                var result = await controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }
    }
}
