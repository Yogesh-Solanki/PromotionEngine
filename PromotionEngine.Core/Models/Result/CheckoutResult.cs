using System.Collections.Generic;

namespace PromotionEngine.Core.Models.Result
{
    public class CheckoutResult
    {
        public IEnumerable<PromotionResult> PromotionResult {get; set;}
        public decimal OrderTotal { get; set; }
    }
}
