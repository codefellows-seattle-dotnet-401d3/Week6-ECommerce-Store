using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new ProductViewModel()
            {
                Products = await _context.Product.Include(p => p.Name).ToListAsync()
            });
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create(
            [Bind("Name", "Price", "Description", "ImagePath")] ProductCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Product product = new Product()
            {
                Name = vm.ProductName,
                Price = vm.ProductPrice,
                Description = vm.ProductDescription,
                ImagePath = vm.ProductImage
            };

            await _context.Product.AddAsync(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return View(vm);
            }

            return RedirectToAction("Details", new { product.Id });
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

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Policy ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImagePath")] Product product)
        {
            if(id != product.Id)
            {
                return NotFound();
            }

            Product updateProduct = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);

            if(ModelState.IsValid && updateProduct != null)
            {
                try
                {
                    updateProduct.Name = product.Name;
                    updateProduct.Price = product.Price;
                    updateProduct.Description = product.Description;
                    updateProduct.ImagePath = product.ImagePath;

                    _context.Update(updateProduct);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return View(updateProduct);
                }

                return RedirectToAction("Details", new { id });
            }

            return View(updateProduct);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(p => p.Id == id);
        }

        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }


    }
}