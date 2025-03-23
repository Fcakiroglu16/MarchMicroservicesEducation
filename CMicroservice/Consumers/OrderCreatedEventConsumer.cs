using BusShared;
using MassTransit;

namespace BMicroservice.Consumers;

public class OrderCreatedEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var version = context.Headers.Get<string>("version");
        var message = context.Message;
        var messageId = context.MessageId;
        //idempotence consumer
        // if(context.inbox.any(x=>x.messageId==messageId))
        //tranaction start
        Console.WriteLine("Consume(C Microservice) Methodu çalıştı");

        //throw new Exception("db error");
        //database error
        Console.WriteLine($"OrderCreatedEventConsumer: {message.OrderCode} - {message.UserId} - {version} - {messageId}");

        //publishEndpoint.Publish(new OrderCreatedEvent(message.OrderCode, message.UserId, message.Items));

        //1. işlem
        //2. işlem

        //publishEndpoint.Publish(new OrderCreatedEvent(message.OrderCode, message.UserId, message.Items));
        //1. işlem
        //2. işlem

        //publishEndpoint.Publish(new OrderCreatedEvent(message.OrderCode, message.UserId, message.Items));

        //inbox table ( messageId,true)
        return Task.CompletedTask;

        //transaction end
    }
}