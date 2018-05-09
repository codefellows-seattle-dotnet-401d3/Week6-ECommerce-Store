using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emusic.Data;
using Emusic.Models;


namespace Emusic.Components
{
    public class BasketDetails : ViewComponent
    {
        private readonly ProductDbContext _productDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasketDetails(ProductDbContext productDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _productDbContext = productDbContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            /* This action allows the view model is set to basket View Model in order to be 
             * called at a
             */
            
            BaskeDetailsViewModel bvm = new BaskeDetailsViewModel();

            long? currentBasketId =
                (await _userManager.GetUserAsync(HttpContext.User))?.CurrentBasketId;

            /* Boolean is the basket filled with items?
             */

            if (currentBasketId.HasValue)
            {
                /* LINQ expression select all product DB context where the basket.ID matches
                 * Product inventory
                 */
   
                bvm.Items = await _productDbContext.Baskets.Where(b => b.Id == currentBasketId)
                                                           .Include(b => b.Items)
                                                           .SelectMany(b => b.Items)
                                                           .Include(bi => bi.Product)
                                                           .ToListAsync();

                /* Basket View model total Line Items
                 */


                bvm.TotalQuantity = bvm.Items.Select(bi => bi.Quantity)
                                             .Sum();

                /* LINQ expression Product DB context total inventory by select items()
                 * Product.Quantity times Product.Price = total value of basket.
                 */

 
                bvm.TotalPrice = bvm.Items.Select(bi => bi.Quantity * bi.Product.Price)
                                          .Sum();
            }

            return View(bvm);
        }



    }
}
