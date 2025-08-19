using System.Globalization;

namespace Shared.Models
{
    public class NumberToken(int skip, double value) : Token(skip)
    {
        public double Value { get; set; } = value;

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
