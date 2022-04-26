using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Services;
using UrlShortener.Domain;

namespace UrlShortener.API.Endpoints;

public static class UrlMappingEndpoints
{
    public static void MapUrlMappingEndpoints(this WebApplication app)
    {
        app.MapPost("/url-mappings", MapUrl);
        app.MapGet("/url-mappings/all", GetAll);
    }
    public record UrlMappingResponseDto(string RedirectFromUrl, string RedirectToUrl);
    private static async Task<IResult> GetAll(IUrlMappingRepository repository, IRedirectUrlService redirectUrlService)
    {
        var urlMappings = await repository.GetAll();
        var result =  urlMappings.Select(x =>
        {
            var redirectFrom = redirectUrlService.GetRedirectUrlForCode(x.Code);
            var redirectTo = x.Url.Value;
            return new UrlMappingResponseDto(redirectFrom, redirectTo);
        });
        return Results.Ok(result);
    }
    
    private static async Task<IResult> MapUrl([FromBody]UrlMappingRequestDto requestDto, IUrlMappingRepository repository, IUrlMappingFactory urlMappingFactory, IRedirectUrlService redirectUrlService)
    {
        var url = new Url(requestDto.Url);
        var entity = await urlMappingFactory.Create(url);
        await repository.Insert(entity);
        var redirectUrl = redirectUrlService.GetRedirectUrlForCode(entity.Code);
        return Results.Ok(redirectUrl);
    }
    public record UrlMappingRequestDto(string Url);
    
}