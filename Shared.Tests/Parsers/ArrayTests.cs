using Shared.Models;

namespace Shared.Tests.Parsers;

public class ArrayTests
{
    [Fact]
    public void Parse_EmptyArray_ReturnsArrayTokenWithNoElements()
    {
        var token = Shared.Parsers.Array.Parse("[]");
        Assert.NotNull(token);
        Assert.Empty(token.Elements);
        Assert.Equal(2, token.Skip);
    }

    [Fact]
    public void Parse_ArrayOfNumbers_ReturnsArrayTokenWithCorrectElements()
    {
        var token = Shared.Parsers.Array.Parse("[1,2,3]");
        Assert.NotNull(token);
        Assert.Equal(3, token.Elements.Count());
        Assert.All(token.Elements, e => Assert.IsType<NumberToken>(e));
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_ArrayOfStrings_ReturnsArrayTokenWithCorrectElements()
    {
        var token = Shared.Parsers.Array.Parse("[\"a\",\"b\"]");
        Assert.NotNull(token);
        Assert.Equal(2, token.Elements.Count());
        Assert.All(token.Elements, e => Assert.IsType<StringToken>(e));
        Assert.Equal(9, token.Skip);
    }

    [Fact]
    public void Parse_MixedArray_ReturnsArrayTokenWithMixedTypes()
    {
        var token = Shared.Parsers.Array.Parse("[1,\"a\",true]");
        Assert.NotNull(token);
        Assert.Equal(3, token.Elements.Count());
        Assert.IsType<NumberToken>(token.Elements.ElementAt(0));
        Assert.IsType<StringToken>(token.Elements.ElementAt(1));
        Assert.IsType<TrueToken>(token.Elements.ElementAt(2));
        Assert.Equal(12, token.Skip);
    }

    [Fact]
    public void Parse_NestedArray_ReturnsArrayTokenWithArrayElement()
    {
        var token = Shared.Parsers.Array.Parse("[[1],2]");
        Assert.NotNull(token);
        Assert.Equal(2, token.Elements.Count());
        Assert.IsType<ArrayToken>(token.Elements.ElementAt(0));
        Assert.IsType<NumberToken>(token.Elements.ElementAt(1));
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_InvalidArray_ThrowsException()
    {
        Assert.Throws<System.Exception>(() => Shared.Parsers.Array.Parse("[1,]"));
    }
}
