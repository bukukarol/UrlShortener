namespace UrlShortener.Domain;

public class UrlMapping
{
    public int Id { get;  }
    public Code Code { get; }  
    public Url Url { get; }
    
#pragma warning disable CS8618
    private UrlMapping(){}
#pragma warning restore CS8618

    public UrlMapping(Code code,Url url)
    {
        Code = code;
        Url = url;
    }
}