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

        public BasketController(ProductDbContext productDbContext, UserManager<ApplicationUser> userManager)
        {
            _productDbContext = productDbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Method to establish the index of the current basket. This should be what is seen from the view component
        /// </summary>
        /// <returns>Returns a view with view model intended to be invoked by the view component </returns>
        public async Task<IActionResult> Index(BasketViewModel bvm)
        {
            //pull the user from the current logged in HttpContext
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //Set our Basket to the current one if it exists
            Basket basket = user.CurrentBasketId.HasValue ?
                await _productDbContext.Baskets.FindAsync(user.CurrentBasketId) : null;

            //Populate our view model with a full basket
            bvm.CurrentBasket = _productDbContext.Baskets.Where(x => x.Id == basket.Id)
                                            .Include(p => p.BasketItems)
                                            .ThenInclude(x => x.Product).First();

            return View(bvm);
        }

        /// <summary>
        /// Method to add an item to the user's current basket. It will also create a basket if the user doesn't currently have one open.
        /// </summary>
        /// <param name="vm">A view model containing the currently desired item</param>
        /// <returns>The return is a redirect to the products index page</returns>
        [HttpPost]
        public async Task<IActionResult> AddItem(BasketAdderViewModel vm)
        {
            //If something slips in the model toss the user back to the store page
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Products");
            }

            //pull the user from the current logged in HttpContext
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //Set our Basket to the current one if it exists
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

            BasketItem item = await _productDbContext.BasketItems.FirstOrDefaultAsync(bi => bi.BasketId == basket.Id);

            // Add the item to the basket or if it already exists add to the quantity
            if (item is null)
            {
                await _productDbContext.BasketItems.AddAsync(new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = vm.ProductId,
                    Quantity = vm.Quantity,
                });
            }
            else
            {
                item.Quantity += vm.Quantity;
                _productDbContext.BasketItems.Update(item);
            }
            return RedirectToAction("Index", "Products");
        }
    }
}
