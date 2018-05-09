using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Components
{
    public class BasketDetails : ViewComponent
    {
        private readonly ProductDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BasketDetails(ProductDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public async Task<IViewComponentResult> InvokeAsync(bool isActive)
        //{
        //    var user = await _userManager.FindByEmailAsync(User.Identity.Name);

        //    var userBasket = _context.Baskets.Where(u => u.UserId == user.Id).Include(b => b.BasketItems).ThenInclude(p => p.Product).First();

        //    return View(userBasket);
        //}
    }
}
