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
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

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
