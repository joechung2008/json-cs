using Shared.Models;

namespace Shared.Tests.Models;

public class TrueTokenTests
{
    [Fact]
    public void ToString_ReturnsTrue()
    {
        var token = new TrueToken(0);
        Assert.Equal("true", token.ToString());
    }
}