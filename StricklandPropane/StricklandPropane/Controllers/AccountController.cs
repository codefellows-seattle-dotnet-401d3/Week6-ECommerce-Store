﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StricklandPropane.Data;
using StricklandPropane.Models;

namespace StricklandPropane.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public List<Claim> GetDefaultClaimsListForUser(ApplicationUser user) => new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.LastName}, {user.FirstName}", ClaimValueTypes.String),
            new Claim(ClaimTypes.Email, user.NormalizedEmail, ClaimValueTypes.Email),
            new Claim(ClaimTypes.StateOrProvince, ((int)user.HomeState).ToString(), ClaimValueTypes.Integer32),
            new Claim(ClaimTypes.StateOrProvince, ((int)user.GrillingPreference).ToString(), ClaimValueTypes.Integer32)
        };

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email,
                    vm.Password, vm.KeepSignedIn, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterCommit(
            [Bind("Email", "Password", "ConfirmPassword", "FirstName", "LastName", "HomeState", "GrillingPreference")] RegisterViewModel vm,
            string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

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
                    GrillingPreference = vm.GrillingPreference,
                    HomeState = vm.HomeState
                };

                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    // Add default claims and member role to the new user
                    await _userManager.AddClaimsAsync(user, GetDefaultClaimsListForUser(user));
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);

                    // Sign the user in and redirect them back to whence they came
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToLocal(returnUrl);
                }

                // Something went wrong. Accumulate all errors into the model state.
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(vm);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // TODO: Actually make some more controllers and views and actions and
                //       whatnot so we can actually direct the user somewhere
                return RedirectToAction("Index", "Home");
            }
        }
    }
}