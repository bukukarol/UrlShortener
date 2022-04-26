namespace UrlShortener.Domain;

public interface IUrlMappingFactory
{
    Task<UrlMapping> Create(Url url);
}

public class UrlMappingFactory : IUrlMappingFactory
{
    private readonly IUrlMappingRepository _repository;
    private readonly ICodeGenerator _codeGenerator;
    private const int CodeGenerationCircuitBreakerThreshold = 100;
    public UrlMappingFactory(IUrlMappingRepository repository, ICodeGenerator codeGenerator)
    {
        _repository = repository;
        _codeGenerator = codeGenerator;
    }
    public async Task<UrlMapping> Create(Url url)
    {
        Code code;
        bool codeAlreadyUsed;
        var loopNr = 0;
        do
        {
            code = _codeGenerator.GetCode();
            codeAlreadyUsed = await _repository.EntityWithCodeExists(code);
            loopNr++;
        } while (codeAlreadyUsed && loopNr< CodeGenerationCircuitBreakerThreshold);
        if (codeAlreadyUsed) throw new Exception("Could not generate unique code for mapping");
        return new UrlMapping(code, url);
    }
}