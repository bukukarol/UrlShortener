using UrlShortener.Domain;

namespace UrlShortener.API.Services;

public interface IRedirectUrlService
{
    string GetRedirectUrlForCode(Code code);
}

public class RedirectUrlService : IRedirectUrlService
{
    private readonly string _baseUrl; 

    public RedirectUrlService(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor?.HttpContext?.Request;
        var host = request?.Host.ToUriComponent();
        var scheme = request?.Scheme;
        _baseUrl = $"{scheme}://{host}";
    }

    public string GetRedirectUrlForCode(Code code)
    {
        return $"{_baseUrl}/r/{code.Value}";
    }
}