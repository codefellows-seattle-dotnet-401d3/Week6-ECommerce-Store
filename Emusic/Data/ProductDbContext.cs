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

        public DbSet<Product> Products {get; set;}

    }
}
