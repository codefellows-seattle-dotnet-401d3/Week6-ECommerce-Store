using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EcommerceStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;



namespace EcommerceStore.Controllers
{
    public class AccountController : Controller
    {
        /*hidden Files for accessing the user info */
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

    }
}
