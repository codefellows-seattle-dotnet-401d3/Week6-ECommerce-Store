using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EcommerceStore.Data;


namespace EcommerceStore.Controllers
{
    public class ProductsController : Controller
    {
        /*This controller should only be accessed if the user is verified
         * CRUD 
         */

        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
