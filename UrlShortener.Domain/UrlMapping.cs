namespace UrlShortener.Domain;

public class UrlMapping
{
    public Code Code { get;  }  
    public Url Url { get;  } 

    public UrlMapping(Code code,Url url)
    {
        Code = code;
        Url = url;
    }
}