using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using EcommerceStore.Data;
using EcommerceStore.Models;


namespace EcommerceStore
{
    public class Startup
    {
        //enable configuration setup, dependancy injectiond
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();


            // adding the Database
            services.AddDbContext<ApplicationDbContext>(options=>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //POSSIBLY ADD A ANOTHER DB CONTEXT FOR PRODUCTS.
            
            /*This is the Configure file for setting up using a Identity database*/
            //Application user- > 
            //IdentityRole -> 
            //Add DefaultTokenProviders- >
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            //This line adds the policy to the role
            //Dependency injection life cycles
            /* singleton -> for the life of the application
             * scoped -> for the lifetime per the session
             * Trainsent -> always being called
             * 
            services.AddAuthorization(options =>
            {
                options.Asddpolicy()
                options.AddPolicy("AdminOnly")
                options-> allows policy requirments inside of here .


            }*/
           
            //
            //services.addtr

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //application. allows read 
            app.UseStaticFiles();
            //application. Authentication usage
            app.UseAuthentication();

            //Home.Route
            app.UseMvcWithDefaultRoute();
       


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Tiger You are at the startup");
            });
        }
    }
}

/* Create a products class and update it into our database 
 * only Authorized members 
 * 
 * *
 */