using Microsoft.AspNetCore.Mvc;

namespace GatewayMicroservice1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpPut("Send")]
        public ActionResult<string> Send()
        {
            return "SendTransaction";
        }
    }
}
