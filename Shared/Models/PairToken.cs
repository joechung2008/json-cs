namespace Shared.Models;

public class PairToken(int skip, StringToken key, Token value) : Token(skip)
{
    public StringToken Key { get; set; } = key;

    public Token Value { get; set; } = value;

    public override string ToString()
    {
        return string.Join("", Key.ToString(), ":", Value.ToString());
    }
}
