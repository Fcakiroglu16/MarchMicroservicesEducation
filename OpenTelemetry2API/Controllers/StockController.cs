using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetry2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok(1);
        }
    }
}
