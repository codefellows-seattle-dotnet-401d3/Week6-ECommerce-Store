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
    public class ModelTests
    {
        [Fact]
        public void CanCreateNewProduct()
        {
            Product test = new Product { Name = "test", Price = 2.99, Description = "this is a test item", ImagePath = "test/path", StudentSale = true, SalePrice = 1.99 };

            Assert.Equal("test", test.Name);
            Assert.Equal(2.99, test.Price);
            Assert.True(test.StudentSale);
        }

        [Fact]
        public void CanCreateWithoutSalePrice()
        {
            Product test = new Product { Name = "test", Price = 2.99, Description = "this is a test item", ImagePath = "test/path", StudentSale = false };

            Assert.Equal("test", test.Name);
            Assert.Equal(2.99, test.Price);
        }
    }
}
