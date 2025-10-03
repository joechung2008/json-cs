using Shared.Models;

namespace Shared.Tests.Models;

public class NumberTokenTests
{
    [Fact]
    public void ToString_Integer_ReturnsStringRepresentation()
    {
        var token = new NumberToken(0, 42);
        Assert.Equal("42", token.ToString());
    }

    [Fact]
    public void ToString_Float_ReturnsStringRepresentation()
    {
        var token = new NumberToken(0, 3.14);
        Assert.Equal("3.14", token.ToString());
    }

    [Fact]
    public void ToString_NegativeNumber_ReturnsStringRepresentation()
    {
        var token = new NumberToken(0, -10);
        Assert.Equal("-10", token.ToString());
    }
}