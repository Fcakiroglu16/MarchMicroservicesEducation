using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetryAPI.Models;
using OpenTelemetryAPI.Services;

namespace OpenTelemetry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context, ILogger<ProductsController> logger, StockService stockService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Get Products");

            var products = context.Products.ToList();


            await stockService.GetStockPrice();

            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var userId = 1000;

            //Activity=Span

            using (var activity2 = ActivitySourceProvider.ActivitySource.StartActivity("DbOperation"))
            {
                activity2!.SetTag("userId", userId);
                activity2.AddEvent(new ActivityEvent("db operation start"));
                //db operations 
                activity2.AddEvent(new ActivityEvent("db operation start"));
            }

            //task
            // quwue mesage

            using (var activity3 = ActivitySourceProvider.ActivitySource.StartActivity("QueueOperation"))
            {
                var a = 2 + 4;
                //queue operations 
            }

            using (var activity4 = ActivitySourceProvider.ActivitySource.StartActivity("RedisOperation"))
            {
                var b = 2 + 4;
                //redis operations 
            }


            using (var activity5 = ActivitySourceProvider.ActivitySource.StartActivity("MathOperation"))
            {
                var c = 2 + 4;
                //Math operations 
            }

                return Ok();
        }
    }
}
