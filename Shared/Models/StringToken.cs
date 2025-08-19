namespace Shared.Models;

public class StringToken(int skip, string value) : Token(skip)
{
    public string Value { get; set; } = value;

    public override string ToString()
    {
        return string.Join("", "\"", Value, "\"");
    }
}
