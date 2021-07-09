using System.Collections.Generic;

namespace PromotionEngine.Core.Factory
{
    public static class SkuConstants
    {
        public static Dictionary<string, decimal> SKUPriceCollection
        {
            get
            {
                return new Dictionary<string, decimal>
                        {
                            { "A", 50 },
                            { "B", 30 },
                            { "C", 20 },
                            { "D", 15 }
                        };
            }
        }
    }
}
