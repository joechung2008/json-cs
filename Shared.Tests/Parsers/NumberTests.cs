using Shared.Models;

namespace Shared.Tests.Parsers;

public class NumberTests
{
    [Fact]
    public void Parse_Integer_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("123");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(123d, token.Value);
        Assert.Equal(3, token.Skip);
    }

    [Fact]
    public void Parse_NegativeInteger_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("-456");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(-456d, token.Value);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_Decimal_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("3.14");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(3.14d, token.Value);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_Zero_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("0");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(0d, token.Value);
        Assert.Equal(1, token.Skip);
    }

    [Fact]
    public void Parse_LargeNumber_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("1234567890");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(1234567890d, token.Value);
        Assert.Equal(10, token.Skip);
    }

    [Fact]
    public void Parse_InvalidNumber_ThrowsException()
    {
        Assert.Throws<System.Exception>(() => Shared.Parsers.Number.Parse("abc"));
    }

    [Fact]
    public void Parse_NumberWithExponent_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("1e3");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(1000d, token.Value);
        Assert.Equal(3, token.Skip);

        token = Shared.Parsers.Number.Parse("-2.5E-2");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(-0.025d, token.Value);
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithTrailingWhitespace_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("42 ");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(42d, token.Value);
        Assert.Equal(2, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithDelimiter_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("77,", new System.Text.RegularExpressions.Regex(","));
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(77d, token.Value);
        Assert.Equal(2, token.Skip);
    }
}
