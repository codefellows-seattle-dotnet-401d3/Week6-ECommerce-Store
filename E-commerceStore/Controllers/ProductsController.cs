using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace EcommerceStore.Controllers
{
    public class ProductsController : Controller
    {
        /*This controller should only be accessed if the user is verified
         * CRUD 
         */

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
