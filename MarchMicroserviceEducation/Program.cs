using AMicroservice.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<BusService>();
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Durable = true;

        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), configure => { });
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