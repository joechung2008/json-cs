using Shared.Models;

namespace Shared.Tests;

public class StringTests
{
    [Fact]
    public void Parse_SimpleString_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"hello\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("hello", token.Value);
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_EmptyString_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("", token.Value);
        Assert.Equal(2, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedQuote_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"he\\\"llo\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("he\"llo", token.Value);
        Assert.Equal(9, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedBackslash_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"he\\\\llo\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("he\\llo", token.Value);
        Assert.Equal(9, token.Skip);
    }

    [Fact]
    public void Parse_StringWithUnicode_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"hi\\u0041\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("hiA", token.Value);
        Assert.Equal(10, token.Skip);
    }

    [Fact]
    public void Parse_InvalidString_ThrowsException()
    {
        Assert.Throws<System.Exception>(() => Shared.Parsers.String.Parse("hello"));
    }
}
