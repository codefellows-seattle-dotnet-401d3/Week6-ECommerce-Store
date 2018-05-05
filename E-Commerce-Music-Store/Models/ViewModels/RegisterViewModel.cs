using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Music_Store.Models;
using E_Commerce_Music_Store.Controllers;
using E_Commerce_Music_Store.Data;
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Music_Store.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "input password", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name ="CofirmPassword")]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }



        [Required]
        public string MusicType { get; set;}



    } //Bottom of the RegisterViewModel
}
