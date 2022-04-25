using UrlShortener.Domain.SeekWork;

namespace UrlShortener.Domain;

public class Url:ValueObject
{
    public Uri Value { get; }
    public Url(string value)
    {
        var isValid = Uri.TryCreate(value, UriKind.Absolute, out var result);
        if (!isValid)
            throw new ArgumentException("Invalid url format");
        Value = result!;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new[] { Value };
    }
}