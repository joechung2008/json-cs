using Shared.Models;

namespace Shared.Tests.Models;

public class FalseTokenTests
{
    [Fact]
    public void ToString_ReturnsFalse()
    {
        var token = new FalseToken(0);
        Assert.Equal("false", token.ToString());
    }
}