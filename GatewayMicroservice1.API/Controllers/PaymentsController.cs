using Microsoft.AspNetCore.Mvc;

namespace GatewayMicroservice1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPayments()
        {
            // Simulate fetching payments from a database or service
            var payments = new List<string>
            {
                "Payment 1",
                "Payment 2",
                "Payment 3"
            };
            return Ok(payments);
        }

        [HttpPost("create")]
        public IActionResult CreatePayment()
        {
            return Ok("Create Payment");
        }
    }
}
