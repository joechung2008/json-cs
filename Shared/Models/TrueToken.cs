namespace Shared.Models;

public class TrueToken(int skip) : Token(skip)
{
    public override string ToString()
    {
        return bool.TrueString.ToLower();
    }
}
