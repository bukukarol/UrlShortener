using UrlShortener.Domain;

namespace UrlShortener.Data;

public class UrlMappingRepository : IUrlMappingRepository
{
    public Task<IEnumerable<UrlMapping>> GetAll()
    {
        var mockArray = new[]
        {
            new UrlMapping(new Code("abcde"), new Url("https://foo.com")),
            new UrlMapping(new Code("abcdf"), new Url("https://foo.com")),
            new UrlMapping(new Code("abcdg"), new Url("https://foo.com")),
        }.AsEnumerable();
            
        return Task.FromResult(mockArray);
    }

    public Task<UrlMapping> GetByCode(Code code)
    {
        return Task.FromResult(new UrlMapping(code, new Url("https://foo.com")));
    }

    public Task Insert(UrlMapping entity)
    {
        return Task.FromResult(new Code("abcde"));
    }
}