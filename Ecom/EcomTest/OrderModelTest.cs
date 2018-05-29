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
    public class OrderModelTest
    {
        [Fact]
        public void CheckId()
        {
            int testValue = 5;
            Order testOrder = new Order() {Id= testValue };

            Assert.Equal(testValue, testOrder.Id);
        }

        [Fact]
        public void CheckUserId()
        {
            string testValue = "user id";
            Order testOrder = new Order() { UserId = testValue };

            Assert.Equal(testValue, testOrder.UserId);
        }

        //Needs to redactor to test contains
        //[Fact]
        //public void CheckOrderItems()
        //{
        //    int testValue = 5;
        //    OrderItem testItem = new OrderItem() { Id = testValue };
        //    Order testOrder = new Order();
        //    testOrder.OrderItems.Add(testItem);

        //    Assert.Contains(testItem, testOrder.OrderItems);
        //}

        [Fact]
        public void CheckTotal()
        {
            double testValue = 5.0;
            Order testOrder = new Order() { Total = testValue };

            Assert.Equal(testValue, testOrder.Total);
        }
    }
}
