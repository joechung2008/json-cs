using Shared.Models;

namespace Shared.Tests.Parsers;

public class ValueParserTests
{
    [Fact]
    public void Parse_Number_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Value.Parse("42");
        Assert.NotNull(token);
        Assert.IsType<NumberToken>(token);
        Assert.Equal(2, token.Skip);
    }

    [Fact]
    public void Parse_String_ReturnsStringToken()
    {
        var token = Shared.Parsers.Value.Parse("\"hello\"");
        Assert.NotNull(token);
        Assert.IsType<StringToken>(token);
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_True_ReturnsTrueToken()
    {
        var token = Shared.Parsers.Value.Parse("true");
        Assert.NotNull(token);
        Assert.IsType<TrueToken>(token);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_False_ReturnsFalseToken()
    {
        var token = Shared.Parsers.Value.Parse("false");
        Assert.NotNull(token);
        Assert.IsType<FalseToken>(token);
        Assert.Equal(5, token.Skip);
    }

    [Fact]
    public void Parse_Null_ReturnsNullToken()
    {
        var token = Shared.Parsers.Value.Parse("null");
        Assert.NotNull(token);
        Assert.IsType<NullToken>(token);
        Assert.Equal(4, token.Skip);
    }

    [Fact]
    public void Parse_Object_ReturnsObjectToken()
    {
        var token = Shared.Parsers.Value.Parse("{\"a\":1}");
        Assert.NotNull(token);
        Assert.IsType<ObjectToken>(token);
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_Array_ReturnsArrayToken()
    {
        var token = Shared.Parsers.Value.Parse("[1,2]");
        Assert.NotNull(token);
        Assert.IsType<ArrayToken>(token);
        Assert.Equal(5, token.Skip);
    }

    [Fact]
    public void Parse_InvalidValue_ThrowsException()
    {
        Assert.Throws<Exception>(() => Shared.Parsers.Value.Parse("???"));
    }

    [Fact]
    public void Parse_ArrayWithLeadingWhitespace_ReturnsArrayToken()
    {
        var token = Shared.Parsers.Value.Parse("  [1,2]");
        Assert.IsType<Shared.Models.ArrayToken>(token);
    }

    [Fact]
    public void Parse_NumberWithLeadingWhitespace_ReturnsNumberToken()
    {
        var token = Shared.Parsers.Value.Parse("  42");
        Assert.IsType<Shared.Models.NumberToken>(token);
        Assert.Equal(42d, ((Shared.Models.NumberToken)token).Value);
    }

    [Fact]
    public void Parse_ObjectWithLeadingWhitespace_ReturnsObjectToken()
    {
        var token = Shared.Parsers.Value.Parse("  {\"a\":1}");
        Assert.IsType<Shared.Models.ObjectToken>(token);
    }
}
