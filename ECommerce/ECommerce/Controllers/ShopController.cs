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
    public class ShopController : Controller
    {
        private ProductDbContext _context;

        public ShopController(ProductDbContext context)
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

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            Product product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Policy = "Student")]
        public async Task<IActionResult> Student()
        {
            return View(new ProductViewModel()
            {
                Products = await _context.Product.ToListAsync()
            });
        }
    }
}
