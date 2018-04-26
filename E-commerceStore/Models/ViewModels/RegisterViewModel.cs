using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Models.ViewModels
{
    /* adding the models to the views in the set.
     */

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
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        //matching of the passwords
        [Required]
        [StringLength(20, ErrorMessage = "Please enter something", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        //Confirmation of the passwords
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password No match")]
        public string ConfirmPassword { get; set; }

        //Adding Items here 


    }
}
