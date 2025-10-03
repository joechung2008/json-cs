using Shared.Models;

namespace Shared.Tests.Parsers;

public class ObjectTests
{
    [Fact]
    public void Parse_EmptyObject_ReturnsObjectTokenWithZeroMembers()
    {
        var token = Shared.Parsers.Object.Parse("{}");
        Assert.NotNull(token);
        Assert.Empty(token.Members);
        Assert.Equal(2, token.Skip);
    }

    [Fact]
    public void Parse_ObjectWithOneMember_ReturnsObjectTokenWithOneMember()
    {
        var token = Shared.Parsers.Object.Parse("{\"a\":1}");
        Assert.NotNull(token);
        Assert.Single(token.Members);
        Assert.Equal(7, token.Skip);
    }

    [Fact]
    public void Parse_ObjectWithMultipleMembers_ReturnsObjectTokenWithCorrectMembers()
    {
        var token = Shared.Parsers.Object.Parse("{\"a\":1,\"b\":2}");
        Assert.NotNull(token);
        Assert.Equal(2, token.Members.Count());
        Assert.Equal(13, token.Skip);
    }

    [Fact]
    public void Parse_ObjectWithWhitespace_IgnoresWhitespace()
    {
        var token = Shared.Parsers.Object.Parse("{  \"a\"  :  1  }");
        Assert.NotNull(token);
        Assert.Single(token.Members);
        Assert.Equal(15, token.Skip);
    }

    [Fact]
    public void Parse_InvalidObject_ThrowsException()
    {
        Assert.Throws<Exception>(() => Shared.Parsers.Object.Parse("{a:1}"));
    }

    [Fact]
    public void Parse_ObjectWithNewlinesAndTabs_ReturnsObjectToken()
    {
        var token = Shared.Parsers.Object.Parse("{\n\t\"key\"\t:\n\t\"value\"\n\t}");
        Assert.NotNull(token);
        Assert.Single(token.Members);
        Assert.Equal("key", token.Members.First().Key.Value);
        Assert.Equal("value", ((StringToken)token.Members.First().Value).Value);
        Assert.Equal(22, token.Skip);
    }

    [Fact]
    public void Parse_ObjectWithLeadingWhitespace_ReturnsObjectToken()
    {
        var token = Shared.Parsers.Object.Parse("  {\"a\":1}");
        Assert.NotNull(token);
        Assert.Single(token.Members);
        Assert.Equal(9, token.Skip);
    }
}
