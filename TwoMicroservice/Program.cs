using BMicroservice.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();

    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Durable = true;

        //1. seviye
        //cfg.UseMessageRetry(r => r.Intervals(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(8), TimeSpan.FromSeconds(16)));

        cfg.UseMessageRetry(r =>
            r.Incremental(3, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4)).Handle(typeof(DivideByZeroException))
        );

        //2. seviye
        cfg.UseDelayedRedelivery(r => r.Incremental(3, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60)).Handle(typeof(DivideByZeroException)));
        //2. seviye
        //3. seviye
        cfg.UseInMemoryOutbox(context);


        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), configure => { });

        // <microservice.name>-<event-name>
        cfg.ReceiveEndpoint("b.microservice.order.created.event", endpoint => { endpoint.ConfigureConsumer<OrderCreatedEventConsumer>(context); });
    });
});


//Repository => ef core = scoped
// redis =>  singleton
//couchbase => singleton

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();