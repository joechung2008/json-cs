using Shared.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public partial class String
{
    [GeneratedRegex("[ \\n\\r\\t]")]
    private static partial Regex GetWhitespaceRegex();

    enum Mode
    {
        Scanning,
        LeftQuote,
        Char,
        EscapedChar,
        Unicode,
        End
    }

    public static StringToken Parse(string s)
    {
        var mode = Mode.Scanning;
        var pos = 0;
        string value = "";

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
                    else if (ch == "\"")
                    {
                        mode = Mode.LeftQuote;
                    }
                    else
                    {
                        throw new Exception($"Expected '\"', actual '{ch}'");
                    }
                    break;

                case Mode.LeftQuote:
                    if (ch == "\"")
                    {
                        value = "";
                        pos++;
                        mode = Mode.Char;
                    }
                    else
                    {
                        throw new Exception($"Expected '\"', actual '{ch}'");
                    }
                    break;

                case Mode.Char:
                    if (ch == "\\")
                    {
                        pos++;
                        mode = Mode.EscapedChar;
                    }
                    else if (ch == "\"")
                    {
                        pos++;
                        mode = Mode.End;
                    }
                    else if (ch != "\n" && ch != "\r")
                    {
                        value += ch;
                        pos++;
                    }
                    else
                    {
                        throw new Exception($"Unexpected character '{ch}'");
                    }
                    break;

                case Mode.EscapedChar:
                    if (ch == "\\" || ch == "\"" || ch == "/")
                    {
                        value += ch;
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "b")
                    {
                        value += "\b";
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "f")
                    {
                        value += "\f";
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "n")
                    {
                        value += "\n";
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "r")
                    {
                        value += "\r";
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "t")
                    {
                        value += "\t";
                        pos++;
                        mode = Mode.Char;
                    }
                    else if (ch == "u")
                    {
                        pos++;
                        mode = Mode.Unicode;
                    }
                    break;

                case Mode.Unicode:
                    var slice = s.Substring(pos, 4);
                    var hex = int.Parse(slice, NumberStyles.HexNumber);
                    value += Convert.ToChar(hex);
                    pos += 4;
                    mode = Mode.Char;
                    break;

                case Mode.End:
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        return new StringToken(pos, value);
    }
}
