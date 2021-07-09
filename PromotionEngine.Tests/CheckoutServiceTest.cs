using FakeItEasy;
using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Interfaces;
using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PromotionEngine.Tests
{
    public class CheckoutServiceTest
    {
        private ILoggerFactory _loggerFactory;
        private readonly IPricingService _pricingService;
        private readonly IPromotionService _promotionService;
        private readonly ICheckoutService _checkoutService;
        

        public CheckoutServiceTest()
        {
            _loggerFactory = A.Fake<ILoggerFactory>();
            _pricingService = new PricingService(_loggerFactory);
            _promotionService = new PromotionService(_loggerFactory);
            _checkoutService = new CheckoutService(_loggerFactory, _pricingService, _promotionService);
        }


        [Fact]
        public void TestScenario1()
        {
            var request = new List<CheckoutRequest>() { 
                new CheckoutRequest { SKU = "A", Quantity = 1 }, 
                new CheckoutRequest { SKU = "B", Quantity = 1 }, 
                new CheckoutRequest { SKU = "C", Quantity = 1} 
            };

            var result = _checkoutService.Checkout(request);

            Assert.NotNull(result);
            Assert.Equal(50, result.PromotionResult.FirstOrDefault(x => x.SKU == "A").Total);
            Assert.Equal(30, result.PromotionResult.FirstOrDefault(x => x.SKU == "B").Total);
            Assert.Equal(20, result.PromotionResult.FirstOrDefault(x => x.SKU == "C").Total);
            Assert.Equal(100, result.OrderTotal);
        }

        [Fact]
        public void TestScenario2()
        {
            var request = new List<CheckoutRequest>() {
                new CheckoutRequest { SKU = "A", Quantity = 5 },
                new CheckoutRequest { SKU = "B", Quantity = 5 },
                new CheckoutRequest { SKU = "C", Quantity = 1}
            };

            var result = _checkoutService.Checkout(request);

            Assert.NotNull(result);
            Assert.Equal(230, result.PromotionResult.FirstOrDefault(x => x.SKU == "A").Total);
            Assert.Equal(120, result.PromotionResult.FirstOrDefault(x => x.SKU == "B").Total);
            Assert.Equal(20, result.PromotionResult.FirstOrDefault(x => x.SKU == "C").Total);
            Assert.Equal(370, result.OrderTotal);
        }

        [Fact]
        public void TestScenario3()
        {
            var request = new List<CheckoutRequest>() {
                new CheckoutRequest { SKU = "A", Quantity = 3 },
                new CheckoutRequest { SKU = "B", Quantity = 5 },
                new CheckoutRequest { SKU = "C", Quantity = 1},
                new CheckoutRequest { SKU = "D", Quantity = 1},
            };

            var result = _checkoutService.Checkout(request);

            Assert.NotNull(result);
            Assert.Equal(130, result.PromotionResult.FirstOrDefault(x => x.SKU == "A").Total);
            Assert.Equal(120, result.PromotionResult.FirstOrDefault(x => x.SKU == "B").Total);
            Assert.Equal(0, result.PromotionResult.FirstOrDefault(x => x.SKU == "C").Total);
            Assert.Equal(30, result.PromotionResult.FirstOrDefault(x => x.SKU == "D").Total);
            Assert.Equal(280, result.OrderTotal);
        }

        [Fact]
        public void TestScenario4()
        {
            var request = new List<CheckoutRequest>() {
                new CheckoutRequest { SKU = "A", Quantity = 11 },
                new CheckoutRequest { SKU = "B", Quantity = 11 },
                new CheckoutRequest { SKU = "C", Quantity = 1},
                new CheckoutRequest { SKU = "D", Quantity = 1},
            };

            var result = _checkoutService.Checkout(request);

            Assert.NotNull(result);
            Assert.Equal(490, result.PromotionResult.FirstOrDefault(x => x.SKU == "A").Total);
            Assert.Equal(255, result.PromotionResult.FirstOrDefault(x => x.SKU == "B").Total);
            Assert.Equal(0, result.PromotionResult.FirstOrDefault(x => x.SKU == "C").Total);
            Assert.Equal(30, result.PromotionResult.FirstOrDefault(x => x.SKU == "D").Total);
            Assert.Equal(775, result.OrderTotal);
        }


        [Fact]
        public void TestScenario5()
        {
            var request = new List<CheckoutRequest>() {
                new CheckoutRequest { SKU = "X", Quantity = 11 },
                new CheckoutRequest { SKU = "B", Quantity = 11 },
                new CheckoutRequest { SKU = "C", Quantity = 1},
                new CheckoutRequest { SKU = "D", Quantity = 1},
            };

            var exception = Assert.Throws<Exception>(() => _checkoutService.Checkout(request));

            Assert.NotNull(exception);
            Assert.Equal("SKU X not found in the system", exception.Message);
        }
    }
}
