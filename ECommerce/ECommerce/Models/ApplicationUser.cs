using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastNAme { get; set; }
        public DateTime Birthday { get; set; }
    }

    public static class ApplicationRoles
    {
        public static string Admin => "Admin";
        public static string AdminNormalized => Admin.ToUpper();

        public static string Member => "Member";
        public static string MemberNormalized => Member.ToUpper();
    }

    public enum StarWars{ I, II, III, IV, V, VI, VII, IIX }
}
