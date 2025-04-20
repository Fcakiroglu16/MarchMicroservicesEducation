using Microsoft.AspNetCore.Mvc;

namespace GatewayMicroservice2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDiscounts()
        {
            // Simulate fetching discounts from a database or service
            var discounts = new List<string>
            {
                "Discount 1",
                "Discount 2",
                "Discount 3"
            };
            return Ok(discounts);
        }
    }
}
