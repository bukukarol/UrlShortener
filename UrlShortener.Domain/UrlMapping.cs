namespace UrlShortener.Domain;

public class UrlMapping
{
    public int Id { get;  }
    public Code Code { get; }  
    public Url Url { get; }

    private UrlMapping(){}

    public UrlMapping(Code code,Url url)
    {
        Code = code;
        Url = url;
    }
}