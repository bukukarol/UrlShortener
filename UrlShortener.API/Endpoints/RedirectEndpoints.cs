using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain;

namespace UrlShortener.API.Endpoints;

public static class RedirectEndpoints
{
    public static void MapRedirectEndpoints(this WebApplication app)
    {
        app.MapGet("/r/{code}", RedirectByCode);
    }

    private static async Task<IResult> RedirectByCode([FromRoute]string code, [FromServices] IUrlMappingRepository repository)
    {
        var urlMapping = await repository.GetByCode(new Code(code));
        //TODO: Log information about redirection
        return Results.Redirect(urlMapping.Url.Value);
    }
}