using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ecom.Models;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Ecom.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signinManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signinManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Initial register view
        /// </summary>
        /// <returns>Returns an empty view object</returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Will register a new user and place him in the database. The method will also create and add claims to the user
        /// </summary>
        /// <param name="rvm">A view model passed in from the route</param>
        /// <returns>Return will be an empty view object if results are unsuccessful</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Guild = rvm.Guild,
                    Class = rvm.Class
                };
                //Create the user in the database using the user manager passed in from config
                var result = await _userManager.CreateAsync(user, rvm.Password);
                //If user was added build and add their claims
                if (result.Succeeded)
                {
                    //Send email confirmation
                    var msg = new SendGridMessage();
                    //TODO Set correct from email
                    msg.SetFrom(new EmailAddress("dx@example.com", "SendGrid DX Team"));
                    //Set the to address from the view model
                    msg.AddTo(rvm.Email);
                    //Set subject
                    msg.SetSubject("Ecom Registration successful");
                    //Set content
                    msg.AddContent(MimeType.Text, "Hello World plain text!");
                    msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

                    //Arrange claims
                    List<Claim> myClaims = new List<Claim>();
                    Claim nameClaim = new Claim(ClaimTypes.Name, $"{rvm.FirstName} {rvm.LastName}",
                        ClaimValueTypes.String);
                    Claim emailClaim = new Claim(ClaimTypes.Email, rvm.Email, ClaimValueTypes.Email);
                    Claim clanClaim = new Claim("guildCheck", rvm.Guild.ToString(), ClaimValueTypes.String); 
                    Claim classClaim = new Claim("classCheck", rvm.Class.ToString(), ClaimValueTypes.String);

                    myClaims.Add(nameClaim);
                    myClaims.Add(emailClaim);
                    myClaims.Add(clanClaim);
                    myClaims.Add(classClaim);

                    //Add the claims to the user in the db
                    await _userManager.AddClaimsAsync(user, myClaims);
                    //Add the user tot he memebrs role
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    //Sign the user in
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //Redirect to the home page
                    RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        /// <summary>
        /// Logs the user out and returns them to the home index
        /// </summary>
        /// <returns>Returns a redirect back to the home index</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Base view action for an empty login view
        /// </summary>
        /// <returns>Returns an empty login view</returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //clear all external logins to ensure a new and clean login
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        /// <summary>
        /// Action to log the user in
        /// </summary>
        /// <param name="lvm">View model with login data</param>
        /// <returns>Returns a view object with the bound view model</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);

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

        /// <summary>
        /// OAuth Action allows users to login with 3rd party credentials
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>Returns a Challenge object that can be used to create and confirm a user</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider)
        {
            string redirectUrl = Url.Action(nameof(ExternalLoginCallbackAsync), "Account");
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(
                provider, redirectUrl);

            return Challenge(properties, provider);
        }

        /// <summary>
        /// Callback intended to handle the response from the external OAuth
        /// </summary>
        /// <param name="remoteError">Error response from the external source</param>
        /// <returns>IF all checks fail return to the login view</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallbackAsync(string remoteError = null)
        {
            //If an external error comes back redirect to login
            if (remoteError != null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Get the login info from the sign in manager
            var info = await _signInManager.GetExternalLoginInfoAsync();

            //If the info fails redirect to login
            if (info is null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);

            //If login is successful go to the product index
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Product");
            }

            //Find the user by email once it's been added
            string externalPrincipalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = await _userManager.FindByEmailAsync(externalPrincipalEmail);

            //If the user fails send the user back to the initial OAuth page
            if (user is null)
            {
                return RedirectToAction("ExternalRegister", new { provider = info.LoginProvider, email = externalPrincipalEmail });
            }

            //Sign the user in
            if ((await _userManager.AddLoginAsync(user, info)).Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Product");
            }

            //If all else fails return to the login view
            return RedirectToAction(nameof(Login));
        }
    }
}