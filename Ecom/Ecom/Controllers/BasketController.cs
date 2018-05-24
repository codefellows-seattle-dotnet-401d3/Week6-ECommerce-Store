using Ecom.Data;
using Ecom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Controllers
{
    public class BasketController : Controller
    {
        private ProductDbContext _productDbContext;
        private UserManager<ApplicationUser> _userManager;

        public BasketController(ProductDbContext productDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _productDbContext = productDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(BasketAdderViewModel vm)
        {
            //If something slips in the model toss the user back to the store page
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Store");
            }

            //pull the user from the current logged in HttpContext
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            Basket basket = user.CurrentBasketId.HasValue ?
                await _productDbContext.Baskets.FindAsync(user.CurrentBasketId) : null;

            // If the user has no basket open, then create one
            if (basket == null)
            {
                basket = (await _productDbContext.Baskets.AddAsync(new Basket()
                {
                    UserId = user.Id,
                    CheckedOut = false
                })).Entity;

                await _productDbContext.SaveChangesAsync();
                //Set the basket ID for the user
                user.CurrentBasketId = basket.Id;
                await _userManager.UpdateAsync(user);
            }

            return View();
        }
    }
}
