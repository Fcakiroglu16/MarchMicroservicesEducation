using Microsoft.AspNetCore.Mvc;

namespace DockerExample2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XController(ProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await productService.GetProducts());
        }
    }
}
