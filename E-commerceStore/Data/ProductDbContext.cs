using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceStore.Models;
using EcommerceStore.Models.Products;

namespace EcommerceStore.Data
{
    public class ProductDbContext : IdentityDbContext
    {
        public ProductDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
    }
}
