namespace UrlShortener.Domain;

public interface IUrlMappingRepository
{
    Task<IEnumerable<UrlMapping>> GetAll();
    Task<UrlMapping> GetByCode(Code code);
    
    Task Insert(UrlMapping entity);
    
}