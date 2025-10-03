using Shared.Models;

namespace Shared.Tests.Models;

public class NullTokenTests
{
    [Fact]
    public void ToString_ReturnsNull()
    {
        var token = new NullToken(0);
        Assert.Equal("null", token.ToString());
    }
}