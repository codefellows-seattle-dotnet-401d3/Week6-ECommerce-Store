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
    public class BasketControllerTest
    {
        [Fact]
        public async void CannAdd()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();


        }
    }
}
