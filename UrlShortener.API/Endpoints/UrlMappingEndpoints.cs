using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain;

namespace UrlShortener.API.Endpoints;

public static class UrlMappingEndpoints
{
    
    public static void MapUrlMappingEndpoints(this WebApplication app)
    {
        app.MapPost("/url-mappings", MapUrl);
        app.MapGet("/url-mappings/all", GetAll);
    }

    private static async Task<IResult> GetAll(IUrlMappingRepository repository)
    {
        var result = await repository.GetAll();
        //TODO map to proper route
        return Results.Ok(result);
    }

    public record UrlMappingDto(string Url);
    private static async Task<IResult> MapUrl(UrlMappingDto dto, IUrlMappingRepository repository, ICodeGenerator codeGenerator)
    {
        var code = codeGenerator.GetCode();
        var url = new Url(dto.Url);
        var entity = new UrlMapping(code, url);
        await repository.Insert(entity);
        //TODO map to proper route
        return Results.Ok(entity.Code.Value);
    }

    
}