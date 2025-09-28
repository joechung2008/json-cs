using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public static partial class Value
{
    [GeneratedRegex(@"[\-\d]")]
    private static partial Regex GetNumberRegex();

    [GeneratedRegex(@"[ \n\r\t]")]
    private static partial Regex GetWhitespaceRegex();

    enum Mode
    {
        Scanning,
        Array,
        False,
        Null,
        Number,
        Object,
        String,
        True,
        End
    }

    public static Token Parse(string s, Regex? delimiters = null)
    {
        var mode = Mode.Scanning;
        var pos = 0;
        Token? token = null;

        while (pos < s.Length && mode != Mode.End)
        {
            var ch = s.Substring(pos, 1);
            string slice;

            switch (mode)
            {
                case Mode.Scanning:
                    if (GetWhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == "[")
                    {
                        mode = Mode.Array;
                    }
                    else if (ch == "f")
                    {
                        mode = Mode.False;
                    }
                    else if (ch == "n")
                    {
                        mode = Mode.Null;
                    }
                    else if (GetNumberRegex().IsMatch(ch))
                    {
                        mode = Mode.Number;
                    }
                    else if (ch == "{")
                    {
                        mode = Mode.Object;
                    }
                    else if (ch == "\"")
                    {
                        mode = Mode.String;
                    }
                    else if (ch == "t")
                    {
                        mode = Mode.True;
                    }
                    else if (delimiters != null && delimiters.IsMatch(ch))
                    {
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Unexpected character '{ch}'");
                    }
                    break;

                case Mode.Array:
                    slice = s.Substring(pos);
                    token = Array.Parse(slice);
                    pos += token.Skip;
                    mode = Mode.End;
                    break;

                case Mode.False:
                    slice = s.Substring(pos, 5);
                    if (slice == "false")
                    {
                        token = new FalseToken(5);
                        pos += token.Skip;
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Expected 'false', actual '{slice}'");
                    }
                    break;

                case Mode.Null:
                    slice = s.Substring(pos, 4);
                    if (slice == "null")
                    {
                        token = new NullToken(4);
                        pos += token.Skip;
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Expected 'null', actual '{slice}'");
                    }
                    break;

                case Mode.Number:
                    slice = s.Substring(pos);
                    token = Number.Parse(slice, delimiters);
                    pos += token.Skip;
                    mode = Mode.End;
                    break;

                case Mode.Object:
                    slice = s.Substring(pos);
                    token = Object.Parse(slice);
                    pos += token.Skip;
                    mode = Mode.End;
                    break;

                case Mode.String:
                    slice = s.Substring(pos);
                    token = String.Parse(slice);
                    pos += token.Skip;
                    mode = Mode.End;
                    break;

                case Mode.True:
                    slice = s.Substring(pos, 4);
                    if (slice == "true")
                    {
                        token = new TrueToken(4);
                        pos += token.Skip;
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Expected 'true', actual '{slice}'");
                    }
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        if (token == null)
        {
            throw new Exception("value cannot be empty");
        }

        token.Skip = pos;
        return token;
    }
}
