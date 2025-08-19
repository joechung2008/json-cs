namespace Shared.Models
{
    public class ArrayToken(int skip, IEnumerable<Token> elements) : Token(skip)
    {
        public IEnumerable<Token> Elements { get; set; } = elements;

        public override string ToString()
        {
            return string.Join("", "[", string.Join(",", from element in Elements select element.ToString()), "]");
        }
    }
}
