using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain;

namespace UrlShortener.Data;

public class UrlMappingRepository : IUrlMappingRepository
{
    private readonly UrlMappingDbContext _dbContext;
    public UrlMappingRepository(UrlMappingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<UrlMapping>> GetAll()
    {
        var entities = await _dbContext.UrlMappings.ToListAsync();
        return entities;
    }

    public async Task<UrlMapping> GetByCode(Code code)
    {
        var entity = await _dbContext.UrlMappings.SingleOrDefaultAsync(x => x.Code.Equals(code));
        if (entity == null)
            throw new ArgumentException("Not found");
        return entity;
    }

    public async Task<bool> EntityWithCodeExists(Code code)
    {
        return await _dbContext.UrlMappings.AnyAsync(x => Equals(x.Code, code));
    }

    public async Task Insert(UrlMapping entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}