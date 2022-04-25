using System.Text.RegularExpressions;
using UrlShortener.Domain.SeekWork;

namespace UrlShortener.Domain;

public class Code : ValueObject
{
    public string Value { get; }
    public Code(string value)
    {
        Value = value;
        Validate();
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new[] { Value };
    }
    
    private void Validate()
    {
        var regex = new Regex(@"^[a-zA-Z0-9]{5}$");
        if (!regex.Match(Value).Success)
            throw new ArgumentException(Value);
    }
    
    
}