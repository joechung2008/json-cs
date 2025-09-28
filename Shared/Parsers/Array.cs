using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public static partial class Array
{
    [GeneratedRegex(@"[,\]]")]
    private static partial Regex DelimitersRegex();

    [GeneratedRegex(@"[ \n\r\t]")]
    private static partial Regex WhitespaceRegex();

    enum Mode
    {
        Scanning,
        Element,
        Comma,
        End
    }

    public static ArrayToken Parse(string s)
    {
        var elements = new List<Token>();
        var mode = Mode.Scanning;
        var pos = 0;

        while (pos < s.Length && mode != Mode.End)
        {
            var ch = s.Substring(pos, 1);

            switch (mode)
            {
                case Mode.Scanning:
                    if (WhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == "[")
                    {
                        pos++;
                        mode = Mode.Element;
                    }
                    else
                    {
                        throw new Exception($"Expected '[', actual '{ch}'");
                    }
                    break;

                case Mode.Element:
                    if (WhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == "]")
                    {
                        if (elements.Count > 0)
                        {
                            throw new Exception("Unexpected ','");
                        }

                        pos++;
                        mode = Mode.End;
                    }
                    else
                    {
                        var slice = s.Substring(pos);
                        var element = Value.Parse(slice, DelimitersRegex());
                        elements.Add(element);
                        pos += element.Skip;
                        mode = Mode.Comma;
                    }
                    break;

                case Mode.Comma:
                    if (WhitespaceRegex().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == ",")
                    {
                        pos++;
                        mode = Mode.Element;
                    }
                    else if (ch == "]")
                    {
                        pos++;
                        mode = Mode.End;
                    }
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        return new ArrayToken(pos, elements);
    }
}