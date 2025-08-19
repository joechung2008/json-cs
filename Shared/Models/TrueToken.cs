namespace Shared.Models;

public class TrueToken(int skip) : Token(skip)
{
    public bool Value => true;

    public override string ToString()
    {
        return bool.TrueString.ToLower();
    }
}
