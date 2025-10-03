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
        Assert.Throws<Exception>(() => Shared.Parsers.Number.Parse("abc"));
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
    public void Parse_NumberWithPositiveExponent_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("1.5e+2");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(150d, token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithLargeExponent_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("2.3E10");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(23000000000d, token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithNegativeExponent_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("5e-3");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(0.005d, token.Value);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithTrailingSpace_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("42 ");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(42d, token.Value);
        Assert.Equal(3, token.Skip);
    }

    [Fact]
    public void Parse_NumberWithTrailingTab_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("123\t");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(123d, token.Value);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_DecimalWithTrailingNewline_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("3.14\n");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(3.14d, token.Value);
        Assert.Equal(5, token.Skip);
    }

    [Fact]
    public void Parse_ExponentWithTrailingCarriageReturn_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("8.9e2\r");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(890d, token.Value);
        Assert.Equal(6, token.Skip);
    }

    [Fact]
    public void Parse_ExponentWithTrailingSpaces_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("-0.5E-1  ");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(-0.05d, token.Value);
        Assert.Equal(9, token.Skip);
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

    [Fact]
    public void Parse_NumberWithLeadingWhitespace_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Number.Parse("  42");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(42d, token.Value);
        Assert.Equal(4, token.Skip);
    }
}
