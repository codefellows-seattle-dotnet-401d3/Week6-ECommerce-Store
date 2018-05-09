using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProductDbContext>>()))
            {
                if (context.Product.Any())
                {
                    return;
                }

                context.Product.AddRange(
                    new Product
                    {
                        Name = "R2D2 Bag",
                        Price = 49.99,
                        Description = "A wheeled bag that happens to look like R2D2",
                        ImagePath = "/Images/bag.jpg",
                        SalePrice = 39.99,
                        StudentSale = true
                    },

                    new Product
                    {
                        Name = "Bathroom Tablet Stand",
                        Price = 29.99,
                        Description = "A stand that holds your tablet and toilet paper!!",
                        ImagePath = "/Images/bathroom.jpg",
                        StudentSale = false
                    },

                    new Product
                    {
                        Name = "All In One Breakfast Maker",
                        Price = 79.99,
                        Description = "One machine that handles all your breakfast needs",
                        ImagePath = "/Images/breakfast.jpg",
                        SalePrice = 60.99,
                        StudentSale = true
                    },

                    new Product
                    {
                        Name = "Meatball Bubblegum",
                        Price = 4.99,
                        Description = "Gumballs that look like meatballs....",
                        ImagePath = "/Images/bubblegum.jpg",
                        SalePrice = 2.50,
                        StudentSale = true
                    },

                    new Product
                    {
                        Name = "Dog Duck Beak",
                        Price = 9.99,
                        Description = "A plastic beak to put on your dog",
                        ImagePath = "/Images/dog-duck.jpg",
                        StudentSale = false
                    },

                    new Product
                    {
                        Name = "Canned Dragon Meat",
                        Price = 99.99,
                        Description = "The freshest canned dragon meat you will ever find",
                        ImagePath = "/Images/dragon.jpg",
                        StudentSale = false
                    },

                    new Product
                    {
                        Name = "Foot Sweeps",
                        Price = 14.99,
                        Description = "Microfiber sweepers that go on your feet, sweep while you drag your feet around the house",
                        ImagePath = "/Images/pet-sweep.jpg",
                        StudentSale = false
                    },

                    new Product
                    {
                        Name = "Onesie Sweeper",
                        Price = 8.99,
                        Description = "A onesie with microfiber sweeper, so your child can clean for you!",
                        ImagePath = "/Images/sweep.jpg",
                        StudentSale = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
