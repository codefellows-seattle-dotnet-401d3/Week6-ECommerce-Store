using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Emusic.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FavoriteColor { get; set; }
        public string MusicType { get; set; }
    }

    public static class ApplicationRoles
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
    }

    public enum MusicType
    {
        Pop,
        Jazz,
        Rock,
        Alternative,
        Country,
        [Display(Name = "Rythm and blues")] RB,

    }
}
