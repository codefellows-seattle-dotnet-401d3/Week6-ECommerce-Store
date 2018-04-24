using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class SeedMemberRoles
    {
        private const string AdminEmail = "admin@admin.com";
        private const string AdminPassword = "@Test123!";

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name=ApplicationRoles.Admin,
            NormalizedName = ApplicationRoles.Member.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()}
        };
    }
}
