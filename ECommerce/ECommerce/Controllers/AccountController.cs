using ECommerce.Models;
using ECommerce.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var role = await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);

                    if(await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Shop");
                }

                ModelState.AddModelError(string.Empty, "Invalid login");
               
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
         
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    List<Claim> userClaims = new List<Claim>();
                    Claim claimName = new Claim(ClaimTypes.Name, $"{model.FirstName} {model.LastName}", ClaimValueTypes.String);
                    Claim claimEmail = new Claim(ClaimTypes.Email, model.Email, ClaimValueTypes.Email);
                    Claim claimBirth = new Claim(ClaimTypes.DateOfBirth, new DateTime
                        (model.Birthday.Year, model.Birthday.Month, model.Birthday.Day).ToString("u"),
                        ClaimValueTypes.DateTime);
                    if (model.Email.Contains(".edu"))
                    {
                        Claim claimStudent = new Claim(ClaimTypes.Email, model.Email, ClaimValueTypes.Email);
                        userClaims.Add(claimStudent);
                    }
                    

                    userClaims.Add(claimName);
                    userClaims.Add(claimEmail);
                    userClaims.Add(claimBirth);

                    await _userManager.AddClaimsAsync(user, userClaims);
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Profile()
        {
            return View(await _userManager.GetUserAsync(HttpContext.User));
        }

        //[HttpPost]
        //public async Task<IActionResult> EditProfile([Bind("Id,FirstName,LastName,Location,Birthday")] ApplicationUser user)
        //{
        //    ApplicationUser updateUser = await _userManager.GetUserAsync(HttpContext.User);

        //    if (ModelState.IsValid && updateUser != null)
        //    {
        //        try
        //        {
        //            updateUser.FirstName = user.FirstName;
        //            updateUser.LastName = user.LastName;
        //            updateUser.Location = user.Location;
        //            updateUser.Birthday = user.Birthday;

        //            await _userManager.UpdateAsync(updateUser);
        //        }
        //    }
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
