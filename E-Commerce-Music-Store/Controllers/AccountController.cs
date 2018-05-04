using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using E_Commerce_Music_Store.Models;
using Microsoft.Extensions.Logging;

using System.Security.Claims;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce_Music_Store.Controllers
{
    public class AccountController : Controller
    {

        // Hidden files for accessing user info
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser>
            signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        // GET: /<controller>/
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = rvm.Email,
                    NormalizedEmail = rvm.Email.ToLower(),
                    UserName = rvm.Email,
                    NormalizedUserName = rvm.Email.ToLower(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    MusicType = rvm.MusicType,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,

                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    List<Claim> myClaims = new List<Claim>();


                    //Claim 01
                    Claim NameClaim = new Claim(ClaimTypes.Name, $"{rvm.FirstName} {rvm.LastName}",
                        ClaimValueTypes.String);

                    //Claim 02
                    Claim Emailclaim = new Claim(ClaimTypes.Email, rvm.Email, ClaimValueTypes.Email);
                    //Claim 03
                    Claim MusicTypeClaim = new Claim("MusicType", rvm.MusicType.ToString(), ClaimValueTypes.String);


                    //Claim 01
                    myClaims.Add(NameClaim);
                    //Claim 02
                    myClaims.Add(Emailclaim);
                    //Claim 03
                    myClaims.Add(MusicTypeClaim);

                    await _userManager.AddClaimsAsync(user, myClaims);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //If the user is successful Redirects to login
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();

        } //Bottom of Register


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
        }//Bottom of the Login 


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

       
    }//Bottom of the Public Account Controller
}
