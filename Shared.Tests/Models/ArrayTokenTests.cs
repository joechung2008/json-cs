using Shared.Models;

namespace Shared.Tests.Models;

public class ArrayTokenTests
{
    [Fact]
    public void ToString_EmptyArray_ReturnsEmptyBrackets()
    {
        var token = new ArrayToken(0, []);
        Assert.Equal("[]", token.ToString());
    }

    [Fact]
    public void ToString_ArrayWithNumbers_ReturnsFormattedArray()
    {
        var elements = new Token[]
        {
            new NumberToken(0, 1),
            new NumberToken(0, 2),
            new NumberToken(0, 3)
        };
        var token = new ArrayToken(0, elements);
        Assert.Equal("[1,2,3]", token.ToString());
    }

    [Fact]
    public void ToString_ArrayWithStrings_ReturnsFormattedArray()
    {
        var elements = new Token[]
        {
            new StringToken(0, "a"),
            new StringToken(0, "b")
        };
        var token = new ArrayToken(0, elements);
        Assert.Equal("[\"a\",\"b\"]", token.ToString());
    }

    [Fact]
    public void ToString_ArrayWithMixedTypes_ReturnsFormattedArray()
    {
        var elements = new Token[]
        {
            new NumberToken(0, 1),
            new StringToken(0, "test"),
            new TrueToken(0)
        };
        var token = new ArrayToken(0, elements);
        Assert.Equal("[1,\"test\",true]", token.ToString());
    }

    [Fact]
    public void ToString_NestedArray_ReturnsFormattedNestedArray()
    {
        var innerElements = new Token[] { new NumberToken(0, 1) };
        var innerArray = new ArrayToken(0, innerElements);
        var elements = new Token[] { innerArray, new NumberToken(0, 2) };
        var token = new ArrayToken(0, elements);
        Assert.Equal("[[1],2]", token.ToString());
    }
}