using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly ProductDbContext _context;

        public AdminController(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new ProductViewModel()
            {
                Products = await _context.Product.ToListAsync()
            });
        }
    }
}
