using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Emusic.Data;
using Emusic.Models;
using Emusic.Models.Policies;

namespace Emusic.Controllers
{

    /* Basket Controller : Can only be called if login is a member : Idenity
     */
    
        [Authorize(Policy = ApplicationPolicies.MemberOnly)]
        public class BasketController : Controller
        {
            private readonly ProductDbContext _productDbContext;
            private readonly UserManager<ApplicationUser> _userManager;

            public BasketController(ProductDbContext productDbContext,
                UserManager<ApplicationUser> userManager)
            {
                _productDbContext = productDbContext;
                _userManager = userManager;
            }

            /// <summary>
            /// Redirect to Index
            /// </summary>
      
            public IActionResult Index()
            {
                return View();
            }

            /// <summary>
            /// Post Method for adding items to basket
            /// </summary>

            [HttpPost]
            public async Task<IActionResult> AddItem(
                [Bind("ProductId", "Quantity", "ReturnUrl")] BasketAdderViewModel vm)
            {
              
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Shop");
                }

                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //Models.Basket var is set to user basket.id
                Basket basket = user.CurrentBasketId.HasValue ?
                    await _productDbContext.Baskets.FindAsync(user.CurrentBasketId) : null;

                if (basket is null || basket.Closed)
                {
                    basket = (await _productDbContext.Baskets.AddAsync(new Basket()
                    {
                        Closed = false,
                        CreationTime = DateTime.UtcNow,
                        UserId = user.Id
                    })).Entity;

                    await _productDbContext.SaveChangesAsync();
                /* new user has empty basket set basket.it = userbasket.id
                 */

                    user.CurrentBasketId = basket.Id;
                    await _userManager.UpdateAsync(user);
                }

                // Models.Basket items returns the item if found LINQ
                BasketItem item = await _productDbContext.BasketItems.FirstOrDefaultAsync(
                    bi => bi.BasketId == basket.Id &&                                                                    
                    bi.ProductId == vm.ProductId);


            /* items in basket are empty add from product DB to new basket Items
             */

                if (item is null)
                {
                    await _productDbContext.BasketItems.AddAsync(new BasketItem()
                    {
                        BasketId = basket.Id,
                        ProductId = vm.ProductId,
                        Quantity = vm.Quantity,
                        UserId = user.Id
                    });
                }
                else
                {
                    item.Quantity += vm.Quantity;
                    _productDbContext.BasketItems.Update(item);
                }

                /* Await and save the new product context to basket
                 */

                await _productDbContext.SaveChangesAsync();
                return RedirectToLocal(vm.ReturnUrl);
            }

        /* Add Update basket Here
         */

        /* Add Checkout basket Here
        */


        private IActionResult RedirectToLocal(string redirectUrl)
            {
                if (Url.IsLocalUrl(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }

                return RedirectToAction("Index", "Shop");
            }
        }
    
}
