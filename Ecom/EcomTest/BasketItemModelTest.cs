using Ecom;
using Ecom.Controllers;
using Ecom.Data;
using Ecom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace EcomTest
{
    public class BasketItemModelTest
    {
        [Fact]
        public void BasketItemIdCheck()
        {
            int testValue = 5;
            BasketItem testBasketItem = new BasketItem()
            {
                Id = testValue,
            };

            Assert.Equal(testValue, testBasketItem.Id);
        }

        [Fact]
        public void BasketItemBasketIdCheck()
        {
            int testValue = 5;
            BasketItem testBasketItem = new BasketItem()
            {
                BasketId = testValue,
            };

            Assert.Equal(testValue, testBasketItem.BasketId);
        }

        [Fact]
        public void BasketItemProductIdCheck()
        {
            int testValue = 5;
            BasketItem testBasketItem = new BasketItem()
            {
                ProductId = testValue,
            };

            Assert.Equal(testValue, testBasketItem.ProductId);
        }

        [Fact]
        public void BasketItemProductCheck()
        {
            Product testProduct = new Product() { Id = 5};
            BasketItem testBasketItem = new BasketItem()
            {
                Product = testProduct,
            };

            Assert.Equal(testProduct.Id, testBasketItem.Product.Id);
        }

        [Fact]
        public void BasketItemQuentityCheck()
        {
            int testValue = 5;
            BasketItem testBasketItem = new BasketItem()
            {
                Quantity = testValue,
            };

            Assert.Equal(testValue, testBasketItem.Quantity);
        }

    }
}
