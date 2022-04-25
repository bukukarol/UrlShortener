using UrlShortener.Domain.SeekWork;

namespace UrlShortener.Domain;

public class Url:ValueObject
{
    public string Value { get; }
    private static readonly string[] AllowedSchemas = { "http", "https" };

    public Url(string value)
    {
        var isValid = Uri.TryCreate(value, UriKind.Absolute, out var result);
        
        if (!isValid)
            throw new ArgumentException("Invalid url format");

        if (!AllowedSchemas.Contains(result?.Scheme))
            throw new ArgumentException("Not allowed protocol");
        
        Value = result!.ToString();
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new[] { Value };
    }
}