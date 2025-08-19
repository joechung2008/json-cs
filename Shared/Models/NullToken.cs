namespace Shared.Models
{
    public class NullToken(int skip) : Token(skip)
    {
        public object? Value
        {
            [return: System.Diagnostics.CodeAnalysis.MaybeNull]
            get => null;
        }

        public override string ToString()
        {
            return "null";
        }
    }
}
