using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Core.Models.Result;
using System.Collections.Generic;

namespace PromotionEngine.Core.Interfaces
{
    public interface ICheckoutService
    {
        CheckoutResult Checkout(IEnumerable<CheckoutRequest> request);
    }
}
