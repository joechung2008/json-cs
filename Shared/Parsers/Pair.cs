using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public partial class Pair
{
    [GeneratedRegex("[ \\n\\r\\t\\},]")]
    private static partial Regex GetDelimitersRegex();

    [GeneratedRegex("[ \\n\\r\\t]")]
    private static partial Regex GetWhitespaceRegex();

    enum Mode
    {
        Scanning,
        Key,
        Colon,
        Value,
        End
    }

    public static PairToken Parse(string s)
    {
        StringToken? key = null;
        var mode = Mode.Scanning;
        var pos = 0;
        string slice;
        Token? value = null;

        while (pos < s.Length && mode != Mode.End)
        {
            var ch = s.Substring(pos, 1);

            switch (mode)
            {
                case Mode.Scanning:
                    if (GetWhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else
                    {
                        mode = Mode.Key;
                    }
                    break;

                case Mode.Key:
                    slice = s.Substring(pos);
                    key = String.Parse(slice);
                    pos += key.Skip;
                    mode = Mode.Colon;
                    break;

                case Mode.Colon:
                    if (GetWhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == ":")
                    {
                        pos++;
                        mode = Mode.Value;
                    }
                    else
                    {
                        throw new Exception($"Expected ':', actual '{ch}'");
                    }
                    break;

                case Mode.Value:
                    slice = s.Substring(pos);
                    value = Value.Parse(slice, GetDelimitersRegex());
                    pos += value.Skip;
                    mode = Mode.End;
                    break;

                case Mode.End:
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        if (key == null)
        {
            throw new Exception("Invalid pair: missing key");
        }
        else if (value == null)
        {
            throw new Exception("Invalid pair: missing value");
        }
        else
        {
            return new PairToken(pos, key, value);
        }
    }
}
