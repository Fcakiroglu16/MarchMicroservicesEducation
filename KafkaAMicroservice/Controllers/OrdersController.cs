using BusShared;
using Microsoft.AspNetCore.Mvc;

namespace KafkaAMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(KafkaServiceBus kafkaServiceBus) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var orderCreatedEvent = new OrderCreatedEvent("abc", "xxx", [new OrderItem("aa", 1, 100)]);

        await kafkaServiceBus.SendMessage("mytoppic", orderCreatedEvent);

        return Ok();
    }
}