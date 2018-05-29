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
    public class ProductModelTest
    {
        [Fact]
        public void IdCheck()
        {
            int testValue = 5;
            Product testProduct = new Product() {Id = testValue };

            Assert.Equal(testValue, testProduct.Id);
        }

        [Fact]
        public void NameCheck()
        {
            string testValue = "test";
            Product testProduct = new Product() { Name = testValue };

            Assert.Equal(testValue, testProduct.Name);
        }

        [Fact]
        public void DescriptionCheck()
        {
            string testValue = "test";
            Product testProduct = new Product() { Description = testValue };

            Assert.Equal(testValue, testProduct.Description);
        }

        [Fact]
        public void UrlCheck()
        {
            string testValue = "test";
            Product testProduct = new Product() { Url = testValue };

            Assert.Equal(testValue, testProduct.Url);
        }

        [Fact]
        public void CostCheck()
        {
            decimal testValue = 5.0m;
            Product testProduct = new Product() { Cost = testValue };

            Assert.Equal(testValue, testProduct.Cost);
        }
    }
}
