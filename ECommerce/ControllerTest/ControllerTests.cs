using ECommerce.Controllers;
using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace ECommerceTest
{
    public class ControllerTests
    {
        [Fact]
        public void CanGetHomeIndexView()
        {
            HomeController home = new HomeController();

            IActionResult ar = home.Index();

            Assert.IsType<ViewResult>(ar);
        }


        // Produst Controller
        [Fact]
        public async void CanReturnProductIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CanReturnProductCreate()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();

                ProductCreateViewModel vm = new ProductCreateViewModel();
                var controller = new ProductController(context);
                var result = await controller.Create(vm);
                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async void CanReturnProductDetails()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Details(3);
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CannotReturnProductDetailsWithBadID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Details(29);
                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async void CanReturnProductEdit()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Edit(2);
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CannotReturnProductEditWithBadID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Edit(99);
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async void CanReturnProductDelete()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Delete(3);
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CannotReturnProductDeleteWithBadID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ProductController(context);
                var result = await controller.Delete(99);
                Assert.IsType<NotFoundResult>(result);
            }
        }


        // Shop Controller
        [Fact]
        public async void CanReturnShopIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ShopController(context);
                var result = await controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CanReturnShopDetails()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ShopController(context);
                var result = await controller.Details(1);
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async void CannotReturnShopDetailsWithBadID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new ShopController(context);
                var result = await controller.Details(99);
                Assert.IsType<RedirectToActionResult>(result);
            }
        }


        // Admin Controller
        [Fact]
        public async void CanReturnAdminIndex()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                 .UseInMemoryDatabase(databaseName: "testDb")
                 .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            var config = builder.Build();

            using (var context = new ProductDbContext(options))
            {
                context.Product.AddRange(
                    new Product { Name = "test1", Price = 12.99, Description = "first test item", ImagePath = "test/path", StudentSale = false },
                    new Product { Name = "test2", Price = 78.99, Description = "second test item", ImagePath = "test/path2", StudentSale = false },
                    new Product { Name = "test3", Price = 2.99, Description = "third test item", ImagePath = "test/path3", StudentSale = true, SalePrice = 1.99 }
                    );
                context.SaveChanges();
                var controller = new AdminController(context);
                var result = await controller.Index();
                Assert.IsType<ViewResult>(result);
            }
        }

        
    }
}
