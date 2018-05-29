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
    public class BasketModelTest
    {
        // Basket.UserId check
        [Fact]
        public void IdCheck()
        {
            Guid testId = new Guid();
            Basket testBasket = new Basket()
            {
                UserId = $"{testId}",
            };

            Assert.Equal(testId.ToString(), testBasket.UserId);
        }

        // Basket.Total check
        [Fact]
        public void totalCheck()
        {
            double testTotal = 5.5;
            Basket testBasket = new Basket()
            {
                Total = testTotal,
            };

            Assert.Equal(testTotal, testBasket.Total);
        }

        // Basket.checkedOut check
        [Fact]
        public void CheckedOutTest()
        {
            bool test = true;
            Basket testBasket = new Basket()
            {
                CheckedOut = test,
            };

            Assert.Equal(test, testBasket.CheckedOut);
        }
    }
}
