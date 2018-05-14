using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Emusic.Models;
using Emusic.Data;
using Emusic.Models.Policies;
using System.Security.Claims;

namespace Emusic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding MVC
            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProductionString")));


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionString"));
            });

    
            // Policy remember to look at models.Policy at startup

            services.AddAuthorization(options =>
            {

                //These are the roles based policy Member and Administrator 
                options.AddPolicy(ApplicationPolicies.AdminOnly, p => p.RequireRole(ApplicationRoles.Admin));
                options.AddPolicy(ApplicationPolicies.MemberOnly, p => p.RequireRole(ApplicationRoles.Member, ApplicationRoles.Admin));

                //This is the Claims based policy

                options.AddPolicy(ApplicationPolicies.CountryMusicOnly, p => p.RequireClaim(ClaimTypes.StateOrProvince, ((int)Genre.Country).ToString()));

                options.AddPolicy("CountryMusicOnly", policy => policy.RequireClaim("Country"));

                //This is the Present based Policy
                options.AddPolicy(ApplicationPolicies.HeadPhonesOnly, p => p.RequireClaim("MusicVenue", ((int)MusicVenue.ILoveMyHeadPhones).ToString()));
             
               // options.AddPolicy("AtLeast21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));

            });

            // add require HTTPs Insert Here.


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //If you wanted to add a live production database string enter it here.
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=Index}/{id?}"));
        }

        /*
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }*/
    }
}
