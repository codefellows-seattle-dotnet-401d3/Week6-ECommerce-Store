using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emusic.Data;
using Emusic.Models;

namespace Emusic.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// 
    public class ShopController : Controller

    {
        private readonly ProductDbContext _productDbContext;



        /// <summary>
        /// 
        /// </summary>
      

        public ShopController(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public IActionResult Index()
        {
            return View(_productDbContext.Products);
        }



        /// <summary>
        /// 
        /// </summary>
     /*
        public async Task<IAsyncResult> Details(long? id)
        {
            Product product;

            if (!id.HasValue || (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                return NotFound();
            }


            return View(product);

        }
       */


    }
}
