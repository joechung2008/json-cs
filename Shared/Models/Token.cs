namespace Shared.Models;

public abstract class Token(int skip)
{
    public int Skip { get; set; } = skip;
}
