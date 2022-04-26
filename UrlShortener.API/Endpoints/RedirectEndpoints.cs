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
        var codeValueObject = new Code(code);
        
        if (!await repository.EntityWithCodeExists(codeValueObject)) return Results.NotFound();
        
        var urlMapping = await repository.GetByCode(codeValueObject);
        var redirectTo = urlMapping.Url.Value;
        logger.LogInformation($"Redirect to {redirectTo}");
        return Results.Redirect(urlMapping.Url.Value);
    }
}