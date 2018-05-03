using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EcommerceStore.Models;
using EcommerceStore.Data;

namespace EcommerceStore.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProductDbContext>>()))
            {
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                     new Product()
                     {
                         Name = "StingTickets",
                         Description = "A Concert for Sting",
                         ImageHref = "Admit-One-Ticket.jpg",
                         Price = 1.00M
                     },

                      new Product()
                      {
                          Name = "Luke Bryant",
                          Description = "A Concert for the Heart",
                          ImageHref = "Admit-One-Ticket.jpg",
                          Price = 1.00M
                      },

                       new Product()
                       {
                           Name = "Blues Brothers",
                           Description = "A Concert for Sting",
                           ImageHref = "Admit-One-Ticket.jpg",
                           Price = 1.00M
                       }




                );
                context.SaveChanges();
            }
        }

    }
}
