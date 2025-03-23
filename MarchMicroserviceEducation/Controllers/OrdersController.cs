using AMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, StockService stockService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> OrderCheck()
    {
        return Ok(await stockService.GetStockCount(2));
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        // Create order
        await orderService.CreateOrder();
        return Ok("Sipariş oluştu");
    }


    [HttpPost("mass-transit")]
    public async Task<IActionResult> CreateOrderWithMasstransit()
    {
        // Create order
        await orderService.CreateOrderWithMasstransit();
        return Ok("Sipariş oluştu");
    }
}