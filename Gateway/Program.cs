using Gateway;
using Gateway.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");

builder.Services.AddAuthentication("GatewayDefaultSchema").AddJwtBearer(
    "GatewayDefaultSchema", opts =>
    {
        opts.Authority = "http://localhost:8080/realms/AbcCompanyTenant";
        opts.RequireHttpsMetadata = false;


        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8080/realms/AbcCompanyTenant",
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddTransient<IClaimsTransformation, ScopeClaimsTransformer>();
builder.Services.AddOcelot().AddDelegatingHandler<DiscountAudienceValidationHandler>();


var app = builder.Build();
app.UseAuthorization();
app.UseAuthorization();
await app.UseOcelot();
app.MapGet("/", () => "Health check");


app.Run();
