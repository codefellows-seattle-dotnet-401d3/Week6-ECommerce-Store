using System;
using Xunit;
using Ecom;
using Ecom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Ecom.Controllers;
using Ecom.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcomTest
{
    public class ProductsControllerTest
    {
        [Fact]
        public async void  CanReturnIndex()
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
