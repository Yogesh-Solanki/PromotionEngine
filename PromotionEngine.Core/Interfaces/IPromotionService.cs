using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Core.Models.Result;
using System.Collections.Generic;

namespace PromotionEngine.Core.Interfaces
{
    public interface IPromotionService
    {
        IEnumerable<PromotionResult> ApplyPromotions(IEnumerable<CheckoutRequest> checkoutRequest);
    }
}
