using Microsoft.EntityFrameworkCore;
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
builder.Services.AddScoped<IUrlMappingFactory, UrlMappingFactory>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<UrlMappingDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(UrlMappingDbContext));
});

var app = builder.Build();

RedirectEndpoints.MapRedirectEndpoints(app);
app.MapUrlMappingEndpoints();

app.Run();