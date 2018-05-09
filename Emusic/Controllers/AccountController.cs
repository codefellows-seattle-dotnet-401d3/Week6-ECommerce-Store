using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Emusic.Data;
using Emusic.Models;
using Emusic.Models.ViewModels;

namespace Emusic.Controllers
{
    /* Controller for members and Managers
     */

    [Authorize]
    public class AccountController : Controller
    {
        //models.Application <userManager>
        private readonly UserManager<ApplicationUser> _userManager;
        //models.Application <userManager>
        private readonly SignInManager<ApplicationUser> _signInManager;
        //models.Application <userManager>
        private readonly ApplicationDbContext _dbContext;


        /// <summary>
        /// Account Controller takes in models.Application assigns _context
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="dbContext"></param>

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        /// <summary>
        /// calls from User sets claim to item types
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 
        public List<Claim> GetDefaultClaimsListForUser(ApplicationUser user) => new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.LastName}, {user.FirstName}", ClaimValueTypes.String),
            new Claim(ClaimTypes.Email, user.NormalizedEmail, ClaimValueTypes.Email),
            new Claim("MusicType", ((int)user.MusicType).ToString(), ClaimValueTypes.Integer32),
            new Claim("MusicVenue", ((int)user.MusicVenue).ToString(), ClaimValueTypes.Integer32)
        };

        /// <summary>
        /// Route to User Login - > redirects to if (cookie is present)  
        /// </summary>
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginViewModel());
        }

        /// <summary>
        /// Async method if user is found in Application Roles
        /// Part of this code was given by Amanada
        /// </summary>
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email,
                    vm.Password, vm.KeepSignedIn, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(vm.Email);

                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Products");
                    }

                    return RedirectToAction("Index", "Shop");
                }
                if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException();
                }
                if (result.IsLockedOut)
                {
                    throw new NotImplementedException();
                }
            }

            return View(vm);
        }


        /* Add External Login here : Need Async call back route
         */


        /* Add External Login here : Need External Register Here
         */


        /* Add External Login here : Need External Post Method Here.
        */


            /// <summary>
            /// Async Method to redirect a sign-in back to Index
            /// </summary>
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
          
            return RedirectToAction("Index", "Home");
        }



         /// <summary>
         /// Redirects to Register Page
         /// </summary>
   
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

            /// <summary>
            /// Register View Page
            /// </summary>

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterCommit(
            [Bind("Email", "Password", "ConfirmPassword", "FirstName", "LastName", "MusicType", "MusicVenue")] RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = vm.Email,
                    NormalizedEmail = vm.Email.ToLower(),
                    UserName = vm.Email,
                    NormalizedUserName = vm.Email.ToLower(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),

                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    MusicType = vm.MusicType,
                    MusicVenue = vm.MusicVenue
                };

                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                 
                    /* _userManager adds default claim to the new user
                     */
                    await _userManager.AddClaimsAsync(user, GetDefaultClaimsListForUser(user));
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    
                    /* If the user is singed in Redirects back to the page where they were
                     */
   
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    /* Administrator redirects back as a product administrator page
                     */
                  
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Products");
                    }

                    return RedirectToAction("Index", "Shop");
                }

                /* If user has an Identity Error redirect back to Error Description
                 */
            
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(vm);
        }

            /// <summary>
            /// Redirects to Profile view
            /// </summary>
  
        public async Task<IActionResult> Profile()
        {
            return View(await _userManager.GetUserAsync(HttpContext.User));
        }
    }
}

