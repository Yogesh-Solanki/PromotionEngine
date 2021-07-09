using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Interfaces;
using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Infrastructure.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ILogger<CheckoutService> _logger;
        private readonly IPricingService _pricingService;
        private readonly IPromotionService _promotionService;

        public CheckoutService(ILoggerFactory loggerFactory, IPricingService pricingService, IPromotionService promotionService)
        {
            _logger = loggerFactory.CreateLogger<CheckoutService>();
            _pricingService = pricingService;
            _promotionService = promotionService;
        }

        public CheckoutResult Checkout(IEnumerable<CheckoutRequest> request)
        {
            try
            {
                _logger.LogDebug("Performing checkout operation");

                _logger.LogDebug("Getting updated pricing for each SKU");
                request?.ToList()?.ForEach(x => { x.Price = _pricingService.GetPrice(x.SKU); });

                
                var result = _promotionService.ApplyPromotions(request);

                var checkoutResult = new CheckoutResult();
                checkoutResult.PromotionResult = result;
                checkoutResult.OrderTotal = result.Sum(x => x.Total);

                return checkoutResult;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured in checkout. Exception : "+ ex);
                throw;
            }
        }
    }
}
