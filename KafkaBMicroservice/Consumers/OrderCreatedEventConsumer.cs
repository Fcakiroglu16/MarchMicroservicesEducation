using BusShared;
using Confluent.Kafka;
using Kafka.Consumer;

namespace KafkaBMicroservice.Consumers;

public class OrderCreatedEventConsumer : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9094",
            GroupId = "kafka.b-microservice.order.created.event.group_id",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        var consumer = new ConsumerBuilder<string, OrderCreatedEvent>(config).SetValueDeserializer(new CustomBodyDeserializer<OrderCreatedEvent>()).Build();
        consumer.Subscribe("mytoppic");


        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(5000);

            if (consumeResult != null)
                try
                {
                    Console.WriteLine($"Order Code : {consumeResult.Message.Value.OrderCode}");

                    consumer.Commit(consumeResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //loogger offset 17 24 43
                }

            await Task.Delay(10, stoppingToken);
        }
    }
}