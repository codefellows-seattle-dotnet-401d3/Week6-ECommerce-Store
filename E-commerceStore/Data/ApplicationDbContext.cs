using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceStore.Models;

namespace EcommerceStore.Data
{
    /* Creating the database to which to store profiles of authorized users.
     */
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

       


    }
}



        ///  View componets,  Multipule baskets
        

