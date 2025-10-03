using Shared.Models;

namespace Shared.Tests.Models;

public class StringTokenTests
{
    [Fact]
    public void ToString_ReturnsQuotedString()
    {
        var token = new StringToken(0, "hello");
        Assert.Equal("\"hello\"", token.ToString());
    }

    [Fact]
    public void ToString_EmptyString_ReturnsEmptyQuotes()
    {
        var token = new StringToken(0, "");
        Assert.Equal("\"\"", token.ToString());
    }

    [Fact]
    public void ToString_StringWithSpecialChars_ReturnsQuotedString()
    {
        var token = new StringToken(0, "hello\nworld");
        Assert.Equal("\"hello\nworld\"", token.ToString());
    }
}