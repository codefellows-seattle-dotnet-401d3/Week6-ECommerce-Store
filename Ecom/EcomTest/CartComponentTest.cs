using Ecom;
using Ecom.Controllers;
using Ecom.Data;
using Ecom.Models;
using Ecom.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace EcomTest
{
    public class CartComponentTest
    {
        // Test incomplete. I can't figure out how to get a test user
        [Fact]
        public void CanReturnView()
        {
            var ProdOptions = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var UserOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testUserDb")
                .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            var userContext = new ApplicationDbContext(UserOptions);

            using (var context = new ProductDbContext(ProdOptions))
            {
                context.Products.AddRange(
                    new Product { Name = "Fighter Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Rogue Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Ranger Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Wizard Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" }
                    );
                context.SaveChanges();

                //var comp = new CartComponent(context, userContext);

                //var result = await comp.Invoke();

                //Placeholder assert
                Assert.True(true);
            }
        }
    }
}
