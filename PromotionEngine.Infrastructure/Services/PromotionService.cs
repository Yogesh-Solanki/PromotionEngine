using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Interfaces;
using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Infrastructure.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly ILogger<PromotionService> _logger;

        public PromotionService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PromotionService>();
        }

        public IEnumerable<PromotionResult> ApplyPromotions(IEnumerable<CheckoutRequest> request)
        {
            _logger.LogDebug("Applying promotion");
            try
            {
                var requestList = request?.ToList();
                var promotionResult = new List<PromotionResult>();

                requestList?.ForEach(x =>
                {
                    switch (x.SKU)
                    {
                        case "A":
                            GetSKUPromotion(x, 3, 130, promotionResult);
                            break;
                        case "B":
                            GetSKUPromotion(x, 2, 45, promotionResult);
                            break;
                        case "C":
                            AssignSKUDetails(x, promotionResult);
                            break;
                        case "D":
                            AssignSKUDetails(x, promotionResult, false);
                            break;
                        default:
                            break;
                    }
                });

                return promotionResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in applying promotion. Exception - " + ex);
                throw;
            }
        }



        private void GetSKUPromotion(CheckoutRequest x, int promoGroupQty, decimal promoGroupQtyPrice, List<PromotionResult> promotionResultList)
        {
            var promotionResult = new PromotionResult();
            promotionResult.SKU = x.SKU;
            promotionResult.Quantity = x.Quantity;

            var group = x.Quantity / promoGroupQty;
            var groupQty = group * promoGroupQty;
            var independentQty = x.Quantity - groupQty;

            promotionResult.Total = group * promoGroupQtyPrice + independentQty * x.Price;

            promotionResultList.Add(promotionResult);
        }

        private void AssignSKUDetails(CheckoutRequest x, List<PromotionResult> promotionResultList, bool isCSKU = true)
        {
            var promotionResult = new PromotionResult();
            promotionResult.SKU = x.SKU;

            if (isCSKU)
            {
                var dQty = promotionResultList.FirstOrDefault(p => p.SKU == "D")?.Quantity;
                if (dQty > 0)
                {
                    promotionResultList.FirstOrDefault(p => p.SKU == "D").Total = 30;
                    promotionResult.Quantity = x.Quantity;
                    promotionResult.Total = 0;
                }
                else
                {
                    promotionResult.Quantity = x.Quantity;
                    promotionResult.Total = x.Quantity * x.Price;
                }
            }
            else
            {
                var cQty = promotionResultList.FirstOrDefault(p => p.SKU == "C")?.Quantity;
                if (cQty > 0)
                {
                    promotionResultList.FirstOrDefault(p => p.SKU == "C").Total = 0;
                    promotionResult.Quantity = x.Quantity;
                    promotionResult.Total = 30;
                }
                else
                {
                    promotionResult.Quantity = x.Quantity;
                    promotionResult.Total = x.Quantity * x.Price;
                }
            }

            promotionResultList.Add(promotionResult);
        }
    }
}
