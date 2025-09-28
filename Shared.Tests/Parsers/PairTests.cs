using Shared.Models;

namespace Shared.Tests.Parsers;

public class PairTests
{
    [Fact]
    public void Parse_SimplePair_ReturnsPairToken()
    {
        var token = Shared.Parsers.Pair.Parse("\"a\":1");
        Assert.NotNull(token);
        Assert.Equal("a", token.Key.Value);
        Assert.IsType<NumberToken>(token.Value);
        Assert.Equal(5, token.Skip);
    }

    [Fact]
    public void Parse_PairWithWhitespace_ReturnsPairToken()
    {
        var token = Shared.Parsers.Pair.Parse("  \"b\"  :  \"val\"  ");
        Assert.NotNull(token);
        Assert.Equal("b", token.Key.Value);
        Assert.IsType<StringToken>(token.Value);
        Assert.Equal(15, token.Skip);
    }

    [Fact]
    public void Parse_PairWithBooleanValue_ReturnsPairToken()
    {
        var token = Shared.Parsers.Pair.Parse("\"flag\":true");
        Assert.NotNull(token);
        Assert.Equal("flag", token.Key.Value);
        Assert.IsType<TrueToken>(token.Value);
        Assert.Equal(11, token.Skip);
    }

    [Fact]
    public void Parse_PairWithNullValue_ReturnsPairToken()
    {
        var token = Shared.Parsers.Pair.Parse("\"x\":null");
        Assert.NotNull(token);
        Assert.Equal("x", token.Key.Value);
        Assert.IsType<NullToken>(token.Value);
        Assert.Equal(8, token.Skip);
    }

    [Fact]
    public void Parse_PairWithEscapedKey_ReturnsPairToken()
    {
        var token = Shared.Parsers.Pair.Parse("\"a\\\"b\":2");
        Assert.NotNull(token);
        Assert.Equal("a\"b", token.Key.Value);
        Assert.IsType<NumberToken>(token.Value);
        Assert.Equal(8, token.Skip);
    }

    [Fact]
    public void Parse_InvalidPair_ThrowsException()
    {
        Assert.Throws<Exception>(() => Shared.Parsers.Pair.Parse("a:1"));
    }
}
