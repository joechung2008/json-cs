using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public static partial class Object
{
    [GeneratedRegex(@"[ \n\r\t]")]
    private static partial Regex GetWhitespaceRegex();

    enum Mode
    {
        Scanning,
        Pair,
        Delimiter,
        End
    }

    public static ObjectToken Parse(string s)
    {
        var mode = Mode.Scanning;
        var pos = 0;
        var members = new List<PairToken>();

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
                    else if (ch == "{")
                    {
                        pos++;
                        mode = Mode.Pair;
                    }
                    else
                    {
                        throw new Exception($"Expected '{{', actual '{ch}'");
                    }
                    break;

                case Mode.Pair:
                    if (GetWhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == "}")
                    {
                        if (members.Any())
                        {
                            throw new Exception("Unexpected ','");
                        }

                        pos++;
                        mode = Mode.End;
                    }
                    else
                    {
                        var slice = s.Substring(pos);
                        var pair = Pair.Parse(slice);
                        members.Add(pair);
                        pos += pair.Skip;
                        mode = Mode.Delimiter;
                    }
                    break;

                case Mode.Delimiter:
                    if (GetWhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == ",")
                    {
                        pos++;
                        mode = Mode.Pair;
                    }
                    else if (ch == "}")
                    {
                        pos++;
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Expected ',' or '}}', actual '{ch}'");
                    }
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        return new ObjectToken(pos, members);
    }
}
