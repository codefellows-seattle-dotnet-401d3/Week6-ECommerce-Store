using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emusic.Models;
using Microsoft.EntityFrameworkCore;

namespace Emusic.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        /*Creating the Database sets 
         */

        //Emusic.Models.Products
        public DbSet<Product> Products { get; set; }
        //Emusic.Models.Basket
        public DbSet<Basket> Baskets { get; set; }
        //Emusic.Models.BasketItems
        public DbSet<BasketItem> BasketItems { get; set; }



        /* Solution Set for creating a basket.
         */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.Items)
                .WithOne(bi => bi.Basket)
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Basket)
                .WithMany(b => b.Items)
                .HasForeignKey(bi => bi.BasketId)
                .IsRequired(true);
        }


    }
}
