using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EcommerceStore.Models
{
    //this is the startup model for Identity, startup profiles
    public class ApplicationUser : IdentityUser
    {
        /* adding properties to the application user
         * 
         */

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Music { get; set; }
        public string MusicType { get; set; }
    }

    public static class ApplicationRoles
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
    }

    public enum MusicType { Country, Pop, Rock, Jazz }
    

}
