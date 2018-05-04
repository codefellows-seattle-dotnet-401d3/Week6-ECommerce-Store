using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Music_Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

       public string MusicType { get; set; }
    }


    public static class ApplicationRoles
    {
        public const string Admin = "Admin";
        public const string Memeber = "Member";
    }

    public enum MusicType {Country, Pop, HipHop}
}
