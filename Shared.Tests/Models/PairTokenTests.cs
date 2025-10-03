using Shared.Models;

namespace Shared.Tests.Models;

public class PairTokenTests
{
    [Fact]
    public void ToString_ReturnsKeyColonValue()
    {
        var key = new StringToken(0, "key");
        var value = new StringToken(0, "value");
        var token = new PairToken(0, key, value);
        Assert.Equal("\"key\":\"value\"", token.ToString());
    }

    [Fact]
    public void ToString_WithNumberValue_ReturnsFormattedPair()
    {
        var key = new StringToken(0, "number");
        var value = new NumberToken(0, 42);
        var token = new PairToken(0, key, value);
        Assert.Equal("\"number\":42", token.ToString());
    }
}