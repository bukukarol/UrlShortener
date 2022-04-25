
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Domain;

public interface ICodeGenerator
{
    public Code GetCode();
}

public class CodeGenerator:ICodeGenerator
{
    private static readonly RandomNumberGenerator RngProvider = RandomNumberGenerator.Create();

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    public Code GetCode()
    {
        var sb = new StringBuilder();
        for (var n = 0; n < 5; n++)
        {
            sb = sb.Append(GetRandomChar());
        }
        return new Code(sb.ToString());
    }
    
    private static char GetRandomChar()
    {
        var byteArray = new byte[1];
        char c;
        do
        {
            RngProvider.GetBytes(byteArray);
            c = (char)byteArray[0];

        } while (Alphabet.All(x => x != c));
        return c;
    }
}
