using MassTransit;

namespace AMicroservice.Services;

public class BusService(IPublishEndpoint publishEndpoint)
{
    public async Task PublishAsync<TEventOrMessage>(TEventOrMessage message)
    {
        //retry => 3
        //timeout => 30s


        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));


        await publishEndpoint.Publish(message!, pipe =>
        {
            pipe.Headers.Set("version", "1.0.0");
            pipe.SetAwaitAck(true);
            pipe.Durable = true;
            pipe.MessageId = Guid.NewGuid();
            //pipe.TimeToLive = TimeSpan.FromSeconds(30);
        }, cancellationTokenSource.Token);
    }
}