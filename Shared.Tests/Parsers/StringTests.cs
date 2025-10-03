using Shared.Models;

namespace Shared.Tests.Parsers;

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
    public void Parse_StringWithEscapedBackspace_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"a\\bc\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("a\bc", token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedFormFeed_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"a\\fc\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("a\fc", token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedNewline_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"a\\nc\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("a\nc", token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedCarriageReturn_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"a\\rc\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("a\rc", token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_StringWithEscapedTab_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("\"a\\tc\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("a\tc", token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_InvalidString_ThrowsException()
    {
        Assert.Throws<Exception>(() => Shared.Parsers.String.Parse("hello"));
    }

    [Fact]
    public void Parse_StringWithLeadingWhitespace_ReturnsStringToken()
    {
        var token = Shared.Parsers.String.Parse("  \"test\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal("test", token.Value);
        Assert.Equal(8, token.Skip);
    }
}
