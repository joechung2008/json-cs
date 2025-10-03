using Shared.Models;

namespace Shared.Tests.Models;

public class ObjectTokenTests
{
    [Fact]
    public void ToString_EmptyObject_ReturnsEmptyBraces()
    {
        var token = new ObjectToken(0, []);
        Assert.Equal("{}", token.ToString());
    }

    [Fact]
    public void ToString_ObjectWithOnePair_ReturnsFormattedObject()
    {
        var pairs = new PairToken[]
        {
            new PairToken(0, new StringToken(0, "key"), new StringToken(0, "value"))
        };
        var token = new ObjectToken(0, pairs);
        Assert.Equal("{\"key\":\"value\"}", token.ToString());
    }

    [Fact]
    public void ToString_ObjectWithMultiplePairs_ReturnsFormattedObject()
    {
        var pairs = new PairToken[]
        {
            new PairToken(0, new StringToken(0, "a"), new NumberToken(0, 1)),
            new PairToken(0, new StringToken(0, "b"), new NumberToken(0, 2))
        };
        var token = new ObjectToken(0, pairs);
        Assert.Equal("{\"a\":1,\"b\":2}", token.ToString());
    }
}