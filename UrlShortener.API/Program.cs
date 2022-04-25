using UrlShortener.API.Endpoints;
using UrlShortener.Data;
using UrlShortener.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<IUrlMappingRepository, UrlMappingRepository>();

var app = builder.Build();

app.MapRedirectEndpoints();
app.MapUrlMappingEndpoints();

app.Run();