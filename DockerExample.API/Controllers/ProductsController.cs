using DockerExample.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DockerExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(context.Products.ToList());
        }
    }
}
