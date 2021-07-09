using FluentValidation;
using PromotionEngine.Core.Models.Requests;

namespace PromotionEngine.Web.RequestValidator
{
    public class CheckoutRequestValidator : AbstractValidator<CheckoutRequest>
    {
        public CheckoutRequestValidator()
        {
            RuleFor(request => request.SKU).NotEmpty();
            RuleFor(request => request.Quantity).NotEmpty().GreaterThan(0);
        }
    }
}
