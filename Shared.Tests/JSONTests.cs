using Shared.Models;

namespace Shared.Tests;

public class JSONTests
{
    [Fact]
    public void Parse_EmptyArray_ReturnsArrayToken()
    {
        var token = JSON.Parse("[]");
        Assert.IsType<ArrayToken>(token);
        Assert.Empty(((ArrayToken)token).Elements);
    }

    [Fact]
    public void Parse_Number_ReturnsNumberToken()
    {
        var token = JSON.Parse("42");
        Assert.IsType<NumberToken>(token);
        Assert.Equal(42, ((NumberToken)token).Value);
    }

    [Fact]
    public void Parse_String_ReturnsStringToken()
    {
        var token = JSON.Parse("\"hello\"");
        Assert.IsType<StringToken>(token);
        Assert.Equal("hello", ((StringToken)token).Value);
    }

    [Fact]
    public void Parse_BooleanTrue_ReturnsTrueToken()
    {
        var token = JSON.Parse("true");
        Assert.IsType<TrueToken>(token);
    }

    [Fact]
    public void Parse_BooleanFalse_ReturnsFalseToken()
    {
        var token = JSON.Parse("false");
        Assert.IsType<FalseToken>(token);
    }

    [Fact]
    public void Parse_Null_ReturnsNullToken()
    {
        var token = JSON.Parse("null");
        Assert.IsType<NullToken>(token);
    }

    [Fact]
    public void Parse_Object_ReturnsObjectToken()
    {
        var token = JSON.Parse("{\"key\": \"value\"}");
        Assert.IsType<ObjectToken>(token);
        var obj = (ObjectToken)token;
        Assert.Single(obj.Members);
        Assert.Equal("key", obj.Members.First().Key.Value);
        Assert.Equal("value", ((StringToken)obj.Members.First().Value).Value);
    }

    [Fact]
    public void Parse_ComplexArray_ReturnsArrayToken()
    {
        var token = JSON.Parse("[1, \"test\", true]");
        Assert.IsType<ArrayToken>(token);
        var arr = (ArrayToken)token;
        Assert.Equal(3, arr.Elements.Count());
    }
}