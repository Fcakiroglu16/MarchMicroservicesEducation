using AMicroservice.Services;
using MassTransit;
using Polly;
using Polly.Extensions.Http;

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
// HttpClient

//Repository => ef core = scoped
// redis =>  singleton
//couchbase => singleton


var retry = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

//circuit breaker ( basic)
var circuitBreakerPolicy = HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(3, TimeSpan.FromSeconds(15));


// advanced circuit breaker
var advancedCircuitBreakerPolicy = HttpPolicyExtensions.HandleTransientHttpError().AdvancedCircuitBreakerAsync(0.3, TimeSpan.FromSeconds(15), 3, TimeSpan.FromSeconds(30));


//timeout
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));


//combine policies
var combinedPolicy = Policy.WrapAsync(retry, circuitBreakerPolicy, timeoutPolicy);

builder.Services.AddHttpClient<StockService>(x => { x.BaseAddress = new Uri("http://localhost:5126"); }).AddPolicyHandler(combinedPolicy);


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