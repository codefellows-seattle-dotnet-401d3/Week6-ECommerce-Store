using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EcommerceStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using EcommerceStore.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace EcommerceStore.Controllers
{
    public class AccountController : Controller
    {
        /*hidden Files for accessing the user info */
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        /* application controller which brings in sign-in manager, and user
        *
         */
        public AccountController(SignInManager<ApplicationUser>
            signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Action which redirects to Register View Page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //This looks at Models.RegisterViewModels
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            /* this async method takes in items from RegisterViewModels & application users
             */

            //Checks to see If 
            if (ModelState.IsValid)
            {
                //New object iteration enters this model. 
                ApplicationUser user = new ApplicationUser
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    BirthDate = rvm.Birthday,
                    Music = rvm.Music,
                    MusicType = rvm.MusicType

                };

                //on Register view model sends to async results, keeps the password separte from the Application user.
                //CreateAsync tells user-manager to make a query on the rvm.Password, makes sure that the code
                //contiunes to run awaiting the call, makes the call to the external method
                var result = await _userManager.CreateAsync(user, rvm.Password);

                /*IF the user is successful is true Adding instanciate in to a <>list of claims
                 * I  
                 */

                if (result.Succeeded)
                {

                    // Constructor for new Claims
                    List<Claim> myClaims = new List<Claim>();

                    // rvm async method to add the FirstNAme and last name
                    Claim Nameclaim = new Claim(ClaimTypes.Name, $"{rvm.FirstName} {rvm.LastName}",

                        ClaimValueTypes.String);

                    //rvm async method to add email 
                    Claim Emailclaim = new Claim(ClaimTypes.Email, rvm.Email, ClaimValueTypes.Email);

                    //rvm async method to add DOB, create a new birth date time in a certain UTC; universal
                    //sorted time Converts in a string format; date-time.
                    Claim Birthdayclaim = new Claim(ClaimTypes.DateOfBirth, new DateTime
                        (rvm.Birthday.Year, rvm.Birthday.Month, rvm.Birthday.Day).ToString("u"),
                        ClaimValueTypes.DateTime);


                   // Claim MusicFanClaim = new Claim(ClaimTypes.Equals, rvm.Music, ClaimValueTypes.String);



                    Claim MusicFanClaim = new Claim("MusicFanCheck", rvm.Music.ToString(), ClaimValueTypes.String);
                    Claim MusicTypeClaim = new Claim("MusicType", rvm.MusicType.ToString(), ClaimValueTypes.String);


                    // Add Method to add listed above name 
                    myClaims.Add(Nameclaim);
                    myClaims.Add(Emailclaim);
                    myClaims.Add(Birthdayclaim);
                    myClaims.Add(MusicFanClaim);
                    myClaims.Add(MusicTypeClaim);




                    //another calls to the _usermanger and adding claims
                    await _userManager.AddClaimsAsync(user, myClaims);

                    /* Might need to use this later on
                     */
                    // If we decided to attach these claims to an identity. 
                    //ClaimsIdentity ci = new ClaimsIdentity(myClaims);
                    //Claims Identity -> drivers license, boarding passes
                    //Claims Principle -> a collections on identity, main-man *needs better documentation*

                    // Application roles. Member, (ApplicationRoles.Member)
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);


                    //True or False Roles.Member, sign in as a convince, or return URL
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    //
                   return RedirectToAction("Index", "Home"); //REDIRECTS INDEX IN HOME
                }

            }

            // returns register view model
            return View();


        }

        

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);
                    var role = await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);

                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");

                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }
            return View(lvm);

        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //clear all external logins to ensure a new and clean login
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }




    }
}
