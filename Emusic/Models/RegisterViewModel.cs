using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Emusic.Models;
using Emusic.Controllers;
using Emusic.Models.ViewModels;


namespace Emusic.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The Password and comfirmation do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EnumDataType(typeof(Genre))]
        [Display(Name = "MusicType")]
        public Genre MusicType { get; set; }

        [Required]
        [EnumDataType(typeof(MusicVenue))]
        [Display(Name = "MusicVenue")]
        public MusicVenue MusicVenue { get; set; }


    }
}
