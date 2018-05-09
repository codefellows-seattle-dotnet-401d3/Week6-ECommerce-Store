using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emusic.Data;
using Emusic.Models;
using Emusic.Models.Policies;

namespace Emusic.Controllers
{
    /// <summary>
    /// Controller For Products if Authorized User
    /// </summary>

    [Authorize(Policy = ApplicationPolicies.AdminOnly)]
    public class ProductsController : Controller
    {
        private readonly ProductDbContext _productDbContext;

        public ProductsController(ProductDbContext context)
        {
            _productDbContext = context;
        }

        public IActionResult Index()
        {
            return View(_productDbContext.Products);
        }

        public IActionResult Create()
        {
            return View();
        }

            /// <summary>
            /// Binding New 
            /// </summary>
  
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CommitCreate(
            [Bind("Name", "Description", "Price", "ImageHref")] Product newProduct)
        {
            
            if (ModelState.IsValid)
            {
                await _productDbContext.Products.AddAsync(newProduct);

                try
                {
                    //saving changes to the product using Async callback
                    await _productDbContext.SaveChangesAsync();

                
                    return RedirectToAction(nameof(Index));
                }

                //This occurs if Database is failed
                catch (DbUpdateException)
                {
                   
                    ModelState.AddModelError("", "Nope");

                  
                }
            }

            return View(newProduct);
        }

            /// <summary>
            /// Product.Model is valid 
            /// </summary>

        public async Task<IActionResult> Edit(long? id)
        {
            //Models.Product
            Product product;

            //bool is true; allow product to find using Async  
            if (!id.HasValue ||
                (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                // Then redirect to Product{i}
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

            /// <summary>
            /// Post Method for Edit products
            /// </summary>

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommitEdit(long? id)
        {   //If no Id is found You cant do anything
            if (!id.HasValue)
            {
                return NotFound();
            }

            //If a product.Model var exsist then consider async find by id
            Product existingProduct = await _productDbContext.Products.FindAsync(id.Value);

            //
            if (await TryUpdateModelAsync(existingProduct, "",
                p => p.Name, p => p.Description, p => p.Price, p => p.ImageHref))
            {
                try
                {
                    await _productDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nope");
                }
            }

            return View(existingProduct);
        }

            /// <summary>
            /// async Delete method
            /// </summary>

        public async Task<IActionResult> Delete(long? id)
        {
            Product product;

          
            if (!id.HasValue ||
                (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                return NotFound();
            }

            return View(product);
        }

            /// <summary>
            /// confirm delete
            /// </summary>

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> CommitDelete(long? id)
        {
            Product product;

         
            if (!id.HasValue ||
                (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                return NotFound();
            }

            _productDbContext.Products.Remove(product);

            try
            {
                await _productDbContext.SaveChangesAsync();
            }
            catch
            {
              
                return View(product);
            }

            
            return RedirectToAction(nameof(Index));
        }
            
            /// <summary>
            /// Async Details page 
            /// </summary>
  
        public async Task<IActionResult> Details(long? id)
        {
            Product product;

            if (!id.HasValue ||
                (product = await _productDbContext.Products.FindAsync(id.Value)) is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }



    }
}
