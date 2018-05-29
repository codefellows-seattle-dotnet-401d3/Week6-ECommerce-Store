using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly ProductDbContext _context;

        public AdminController(ProductDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to show an index of products that the admin can manipulate
        /// </summary>
        /// <returns>A view object with the list of products as the model</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
    }
}