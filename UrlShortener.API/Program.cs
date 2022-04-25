using UrlShortener.API.Endpoints;
using UrlShortener.API.Services;
using UrlShortener.Data;
using UrlShortener.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddScoped<IRedirectUrlService, RedirectUrlService>();
builder.Services.AddScoped<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<IUrlMappingRepository, UrlMappingRepository>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapRedirectEndpoints();
app.MapUrlMappingEndpoints();

app.Run();