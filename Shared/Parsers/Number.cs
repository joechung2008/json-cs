using Shared.Models;
using System.Text.RegularExpressions;

namespace Shared.Parsers;

public static partial class Number
{
    [GeneratedRegex(@"\d")]
    private static partial Regex Digits();

    [GeneratedRegex("[1-9]")]
    private static partial Regex NonZeroDigits();

    [GeneratedRegex(@"[ \n\r\t]")]
    private static partial Regex Whitespace();

    enum Mode
    {
        Scanning,
        Characteristic,
        CharacteristicDigit,
        DecimalPoint,
        Mantissa,
        Exponent,
        ExponentSign,
        ExponentFirstDigit,
        ExponentDigits,
        End
    }

    public static NumberToken Parse(string s, Regex? delimiters = null)
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
                    if (Whitespace().IsMatch(ch))
                    {
                        pos++;
                    }
                    else if (ch == "-")
                    {
                        pos++;
                        value += "-";
                    }
                    else if (Digits().IsMatch(ch))
                    {
                        mode = Mode.Characteristic;
                    }
                    else
                    {
                        throw new Exception($"Expected '-' or digit, actual '{ch}'");
                    }
                    break;

                case Mode.Characteristic:
                    if (ch == "0")
                    {
                        value += "0";
                        pos++;
                        mode = Mode.DecimalPoint;
                    }
                    else if (NonZeroDigits().IsMatch(ch))
                    {
                        value += ch;
                        pos++;
                        mode = Mode.CharacteristicDigit;
                    }
                    else
                    {
                        throw new Exception($"Expected digit, actual '{ch}'");
                    }
                    break;

                case Mode.CharacteristicDigit:
                    if (Digits().IsMatch(ch))
                    {
                        value += ch;
                        pos++;
                    }
                    else if (delimiters != null && delimiters.IsMatch(ch))
                    {
                        mode = Mode.End;
                    }
                    else
                    {
                        mode = Mode.DecimalPoint;
                    }
                    break;

                case Mode.DecimalPoint:
                    if (ch == ".")
                    {
                        value += ".";
                        pos++;
                        mode = Mode.Mantissa;
                    }
                    else
                    {
                        mode = Mode.Exponent;
                    }
                    break;

                case Mode.Mantissa:
                    if (Digits().IsMatch(ch))
                    {
                        value += ch;
                        pos++;
                    }
                    else
                    {
                        mode = Mode.Exponent;
                    }
                    break;

                case Mode.Exponent:
                    if (ch == "e" || ch == "E")
                    {
                        value += ch;
                        pos++;
                        mode = Mode.ExponentSign;
                    }
                    else if (delimiters != null && delimiters.IsMatch(ch))
                    {
                        mode = Mode.End;
                    }
                    else if (pos >= s.Length - 1)
                    {
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Unexpected character '{ch}'");
                    }
                    break;

                case Mode.ExponentSign:
                    if (ch == "-" || ch == "+")
                    {
                        value += ch;
                        pos++;
                    }

                    mode = Mode.ExponentFirstDigit;
                    break;

                case Mode.ExponentFirstDigit:
                    if (Digits().IsMatch(ch))
                    {
                        value += ch;
                        pos++;
                        mode = Mode.ExponentDigits;
                    }
                    else
                    {
                        throw new Exception($"Expected digit, actual '{ch}'");
                    }
                    break;

                case Mode.ExponentDigits:
                    if (Digits().IsMatch(ch))
                    {
                        value += ch;
                        pos++;
                    }
                    else if (delimiters != null && delimiters.IsMatch(ch))
                    {
                        mode = Mode.End;
                    }
                    else if (Whitespace().IsMatch(ch))
                    {
                        mode = Mode.End;
                    }
                    else
                    {
                        throw new Exception($"Expected digit, actual '{ch}'");
                    }
                    break;

                default:
                    throw new Exception($"Unexpected mode {mode}");
            }
        }

        if (mode == Mode.Characteristic || mode == Mode.ExponentFirstDigit)
        {
            throw new Exception($"Incomplete expression, mode = {mode}");
        }

        // Skip trailing whitespace after parsing the number
        while (pos < s.Length && Whitespace().IsMatch(s.Substring(pos, 1)))
        {
            pos++;
        }

        return new NumberToken(pos, double.Parse(value));
    }
}
