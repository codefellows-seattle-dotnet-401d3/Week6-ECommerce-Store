using Ecom.Data;
using Ecom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> Invoke()
        {
            int? currentBasketId = (await _userManager.GetUserAsync(HttpContext.User))?.CurrentBasketId;
            BasketViewModel bvm = new BasketViewModel()
            {
                CurrentBasket = await _productDbContext.Baskets.FindAsync(currentBasketId),
            };

            return Content("Navigation");
        }
    }
}
