using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pos.DataAccess.Model;
using Pos.Web.Controllers;
using Pos.Web.Models;

namespace Pos.Web.UnitTests
{
    [TestClass]
    public class PriceCalculatorTests
    {
        [TestMethod]
        public void GetPrice_WithVatAndRegionalTax_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "RegionalTax", Value = 0.10m }, new Tax {Name = "VAT", Value = 0.19m} };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes};
            var priceWithRegionalTax = testDataProduct.Price + testDataProduct.Price * 0.10m;
            var expectedPrice = priceWithRegionalTax + priceWithRegionalTax * 0.19m;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithVatTaxOnly_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "VAT", Value = 0.19m } };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes };
            var expectedPrice = testDataProduct.Price + testDataProduct.Price * 0.19m;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithRegionalTaxOnly_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "RegionalTax", Value = 0.10m } };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes };
            var expectedPrice = testDataProduct.Price + testDataProduct.Price * 0.10m;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithVatRegionalAndLuxuryTax_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "RegionalTax", Value = 0.10m }, new Tax { Name = "VAT", Value = 0.19m }, new Tax { Name = "LuxuryTax", Value = 0.10m } };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes };
            var priceWithRegionalTax = testDataProduct.Price + testDataProduct.Price * 0.10m;
            var priceWithRegionalAndVat = priceWithRegionalTax + priceWithRegionalTax * 0.19m;
            var expectedPrice = priceWithRegionalAndVat + priceWithRegionalAndVat * 0.10m;
            
            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithNoTax_PriceIsEqualWithCatalogPrice()
        {
            Tax[] taxes = new Tax[]{};
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes };
            var expectedPrice = testDataProduct.Price;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithDiscount_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "VAT", Value = 0.19m } };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes, Discount = 0.5m};
            var price = testDataProduct.Price + testDataProduct.Price * 0.19m;
            var expectedPrice = price - price * 0.5m;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }

        [TestMethod]
        public void GetPrice_WithDiscountBiggerThanPrice_PriceIsCalculatedCorrectly()
        {
            Tax[] taxes = new Tax[] { new Tax { Name = "VAT", Value = 0.19m } };
            Product testDataProduct = new Product { CatalogCode = "some code", Price = 10, Taxes = taxes, Discount = 1.5m };
            var expectedPrice = 0;

            PriceCalculator target = new PriceCalculator();
            var finalPrice = target.GetPrice(testDataProduct);

            Assert.AreEqual(expectedPrice, finalPrice);
        }
    }
}
