namespace AMicroservice.OutboxBackgroundServices;

public class OutboxJob(IServiceProvider serviceProvider) : BackgroundService
{
    //Singleton
    //Scoped
    //Transient
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //var scope = serviceProvider.CreateScope();
        //var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();


        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    //1. Get all outbox messages
        //    //var outboxMessages = _outboxMessageRepository.GetOutboxMessages();
        //    // outboxMessage foreach
        //    //2. Publish the message
        //}

        return Task.CompletedTask;
        ;
    }
}