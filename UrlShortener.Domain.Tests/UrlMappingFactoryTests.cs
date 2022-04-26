using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace UrlShortener.Domain.Tests;

public class UrlMappingFactoryTests
{
    private readonly IUrlMappingRepository _repositoryMock;
    private readonly ICodeGenerator _codeGeneratorMock;

    public UrlMappingFactoryTests()
    {
        _repositoryMock = Substitute.For<IUrlMappingRepository>();
       
        
        _codeGeneratorMock = Substitute.For<ICodeGenerator>();
        _codeGeneratorMock.GetCode().Returns(new Code("abc12"));
    }
    
    [Fact]
    public async Task Should_create_url_mapping()
    {
        //Arrange
        var code = new Code("abc12");
        var url = new Url("https://foo.com");
        _repositoryMock.EntityWithCodeExists(Arg.Any<Code>()).Returns(false);
        _codeGeneratorMock.GetCode().Returns(code);
        var urlMappingFactory = new UrlMappingFactory(_repositoryMock, _codeGeneratorMock);
        //Act
        var urlMapping = await urlMappingFactory.Create(url);
        //Assert
        urlMapping.Code.Should().Be(code);
        urlMapping.Url.Should().Be(url);
    }
    
    [Fact]
    public async Task Should_throw_exception_if_cannot_create_unique_mapping()
    {
        //Arrange
        var code = new Code("abc12");
        var url = new Url("https://foo.com");
        _repositoryMock.EntityWithCodeExists(Arg.Any<Code>()).Returns(true);
        _codeGeneratorMock.GetCode().Returns(code);
        var urlMappingFactory = new UrlMappingFactory(_repositoryMock, _codeGeneratorMock);
        //Act
        var action = async () =>  await urlMappingFactory.Create(url);
        //Assert
        await action.Should().ThrowAsync<UrlMappingFactory.UrlMappingFactoryCannotGenerateUniqueEntity>();
    }
}