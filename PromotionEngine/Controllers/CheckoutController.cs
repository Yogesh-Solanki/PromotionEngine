using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Core.Interfaces;
using PromotionEngine.Core.Models.Requests;
using PromotionEngine.Core.Models.Result;
using System.Collections.Generic;

namespace PromotionEngine.Controllers
{
    [ApiController]
    [Route("promotion/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<CheckoutResult>))]
        public IActionResult Post([FromBody] IEnumerable<CheckoutRequest> request)
        {
            return Ok(_checkoutService.Checkout(request));
        }
    }
}
