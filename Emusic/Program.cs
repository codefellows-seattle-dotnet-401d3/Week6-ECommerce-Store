using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Emusic.Data;
using Emusic.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Emusic
{
    public class Program
    {
        public static void Main(string[] args)
        {

            IWebHost host = BuildWebHost(args);

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                UserManager<ApplicationUser> userManager =
                    services.GetRequiredService<UserManager<ApplicationUser>>();

                try
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task rolesTask = SeedMemberRoles.Initialize(services, userManager);
                    Task productsTask = SeedProducts.Initialize(services);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                   
                    while (!rolesTask.IsCompleted || !productsTask.IsCompleted) { }
                }
                catch
                {
                    Console.Error.WriteLine(
                        "Could not seed database with admin user and roles.");
                    throw;
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}


/*
            var host = BuildWebHost(args);

            //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                try
                {
                    await SeedProducts.Initialize(services);
                    SeedMemberRoles.SeedData(services, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
 */
