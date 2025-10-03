namespace Shared.Models
{
    public class NullToken(int skip) : Token(skip)
    {
        public override string ToString()
        {
            return "null";
        }
    }
}
