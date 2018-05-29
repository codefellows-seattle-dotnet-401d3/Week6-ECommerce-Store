using Ecom.Data;
using Ecom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//component is incomplete. I can get a simple string to load but anything else seems to throw a "Invoke can't return a task" error
namespace Ecom.Components
{
    public class CartComponent : ViewComponent
    {
        private readonly ProductDbContext _productDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartComponent(ProductDbContext productDbContext, UserManager<ApplicationUser> userManager)
        {
            _productDbContext = productDbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// gets the current cart and displays all of the items in a view component
        /// </summary>
        /// <returns>Task that holds the view and the view model</returns>
        public async Task<IViewComponentResult> Invoke()
        {
            //pull the user from the current logged in HttpContext
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //Set our Basket to the current one if it exists
            Basket basket = user.CurrentBasketId.HasValue ?
                await _productDbContext.Baskets.FindAsync(user.CurrentBasketId) : null;

            //Create our view model and populate it with a full basket
            BasketViewModel bvm = new BasketViewModel()
            {
                CurrentBasket = _productDbContext.Baskets.Where(x => x.Id == basket.Id)
                                            .Include(p => p.BasketItems)
                                            .ThenInclude(x => x.Product).First(),
            };


            return View(bvm);
        }
    }
}
