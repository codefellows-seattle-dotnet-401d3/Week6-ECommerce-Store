using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce_Music_Store.Data;

namespace E_Commerce_Music_Store.Models
{
    public class SeedMemberRoles
    {
        private const string AdminEmail = "email@email.com";
        private const string AdminPassword = "1234";

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
      {
          new IdentityRole{Name=ApplicationRoles.Admin,
              NormalizedName = ApplicationRoles.Admin.ToUpper(),
              ConcurrencyStamp = Guid.NewGuid().ToString()
          },

          new IdentityRole{Name=ApplicationRoles.Memeber,
          NormalizedName = ApplicationRoles.Memeber.ToUpper(),
          ConcurrencyStamp = Guid.NewGuid().ToString()

      }
      };



    }//Bottom of Seed Member Roles
}
