using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenTelemetry().WithTracing(traceProviderBuilder =>
{
    traceProviderBuilder.ConfigureResource(x => { x.AddService("Stock.Microservice", serviceVersion: "1.0.0"); });


    traceProviderBuilder.AddSource("AppActivitySource");

    traceProviderBuilder.AddHttpClientInstrumentation();
    traceProviderBuilder.AddAspNetCoreInstrumentation();
    traceProviderBuilder.AddOtlpExporter();
    traceProviderBuilder.AddConsoleExporter();
}).WithLogging(configureLogging =>
{
    configureLogging.ConfigureResource(x => { x.AddService("Stock.Microservice", serviceVersion: "1.0.0"); });
    configureLogging.AddOtlpExporter();
    configureLogging.AddConsoleExporter();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
