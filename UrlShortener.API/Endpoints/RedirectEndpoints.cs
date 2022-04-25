using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain;

namespace UrlShortener.API.Endpoints;

public class RedirectEndpoints
{
    public static void MapRedirectEndpoints(WebApplication app)
    {
        app.MapGet("/r/{code}", RedirectByCode);
    }
    //TODO: dirty fix of not easy way to use ILogger<> with a static class :/ to rethink 
    private static async Task<IResult> RedirectByCode([FromRoute] string code, [FromServices] IUrlMappingRepository repository, [FromServices] ILogger<RedirectEndpoints> logger)
    {
        var urlMapping = await repository.GetByCode(new Code(code));
        var redirectTo = urlMapping.Url.Value;
        logger.LogInformation($"Redirect to {redirectTo}");
        return Results.Redirect(urlMapping.Url.Value);
    }
}