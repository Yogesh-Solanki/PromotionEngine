namespace PromotionEngine.Core.Models.Requests
{
    public class CheckoutRequest
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
