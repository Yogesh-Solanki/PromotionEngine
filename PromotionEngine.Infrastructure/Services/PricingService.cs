using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Factory;
using PromotionEngine.Core.Interfaces;
using System;

namespace PromotionEngine.Infrastructure.Services
{
    public class PricingService : IPricingService
    {
        private readonly ILogger<PricingService> _logger;
        public PricingService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PricingService>();
        }

        public decimal GetPrice(string sku)
        {
            try
            {
                if (SkuConstants.SKUPriceCollection.ContainsKey(sku))
                {
                    return SkuConstants.SKUPriceCollection[sku];
                }
                
                throw new Exception($"SKU {sku} not found in the system");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception - "+ex);
                throw;
            }
        }
    }
}
