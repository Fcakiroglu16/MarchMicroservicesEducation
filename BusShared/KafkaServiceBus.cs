using System.Text;
using Confluent.Kafka;
using Kafka.Producer;

namespace BusShared;

public class KafkaServiceBus
{
    public async Task SendMessage(string topicName, OrderCreatedEvent orderCreatedEvent)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9094" };

        using var producer = new ProducerBuilder<string, OrderCreatedEvent>(config)
            .SetValueSerializer(new CustomBodySerializer<OrderCreatedEvent>())
            .Build();


        // snow flake => uuid v7 
        var headers = new Headers { { "x-correlation-id", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()) } };

        var message = new Message<string, OrderCreatedEvent>
        {
            Value = orderCreatedEvent,
            Key = orderCreatedEvent.UserId,
            Headers = headers
        };

        var result = await producer.ProduceAsync(topicName, message);
    }
}