using Ecom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class SeedProducts
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductDbContext(serviceProvider.
                GetRequiredService<DbContextOptions<ProductDbContext>>()))
            {
                if (context.Products.Any()) return; // No seed needed

                context.Products.AddRange(
                    new Product { Name = "Fighter Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Rogue Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Ranger Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" },
                    new Product { Name = "Wizard Gear", Description = "All you need", Cost = 10.99m, Url = "http://placehold.it/300x300" }
                    );
                context.SaveChanges();
            }
        }
    }
}
