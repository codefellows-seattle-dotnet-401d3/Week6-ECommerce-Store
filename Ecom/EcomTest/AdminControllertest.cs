using Ecom;
using Ecom.Controllers;
using Ecom.Data;
using Ecom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace EcomTest
{
    public class AdminControllertest
    {
        [Fact]
        public async void CanReturnIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Products.AddRange(
                    new Product { Name = "Fighter Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Rogue Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Ranger Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Wizard Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" }
                    );
                context.SaveChanges();

                var controller = new ProductsController(context);

                var result = await controller.Index();

                Assert.IsAssignableFrom<IActionResult>(result);
            }
        }
    }
}
