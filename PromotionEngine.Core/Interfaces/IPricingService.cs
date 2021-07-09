namespace PromotionEngine.Core.Interfaces
{
    public interface IPricingService
    {
        decimal GetPrice(string sku);
    }
}
