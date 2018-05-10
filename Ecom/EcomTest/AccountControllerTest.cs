using System;
using Xunit;
using Ecom;
using Ecom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Ecom.Controllers;
using Ecom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static Ecom.Program;

namespace EcomTest
{
    public class AccountControllerTest
    {
        //public string[] args { get; set; }
        //[Fact]
        //public void CanBuild()
        //{
        //    var host = BuildWebHost(args);
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        //        SeedMemberRoles.SeedData(services, userManager);
        //        SeedProducts.Initialize(services);
        //    }
        //    host.Run();

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(databaseName: "testDb")
        //        .Options;
        //    var builder = new ConfigurationBuilder().AddEnvironmentVariables();
        //    builder.AddUserSecrets<Startup>();
        //    var configuration = builder.Build();

        //    Assert.True(true);
        //}
    }
}
