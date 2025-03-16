using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using BusShared;
using RabbitMQ.Client;

namespace AMicroservice.Services;

public class OrderService(IConfiguration configuration, ILogger<OrderService> logger, BusService busService) : IOrderService
{
    public async Task CreateOrder()
    {
        var connectionString = configuration.GetConnectionString("RabbitMQ");

        var factory = new ConnectionFactory { Uri = new Uri(connectionString) };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync(new CreateChannelOptions(true, true));
        var orderItems = new List<OrderItem>
        {
            new("789", 1, 100)
        };

        var order = new OrderCreatedEvent("123", "456", orderItems.ToImmutableList());

        var orderAsJson = JsonSerializer.Serialize(order);
        var orderAsBytes = Encoding.UTF8.GetBytes(orderAsJson);

        await channel.ExchangeDeclareAsync("fanout-exchange", ExchangeType.Fanout, true, false);


        await channel.BasicPublishAsync("fanout-exchange", "", true, orderAsBytes).AsTask().ContinueWith(x =>
        {
            if (x.IsFaulted) logger.LogError(x.Exception, "kuyruğa mesaj gönderilemedi");
        });
        Console.WriteLine("Mesaj Gönderildi");
    }

    public async Task CreateOrderWithMasstransit()
    {
        var orderItems = new List<OrderItem>
        {
            new("789", 1, 100)
        };

        var order = new OrderCreatedEvent("123", "456", orderItems.ToImmutableList());

        await busService.PublishAsync(order);
    }
}