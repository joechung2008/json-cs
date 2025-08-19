namespace Shared.Models
{
    public class ObjectToken(int skip, IEnumerable<PairToken> members) : Token(skip)
    {
        public IEnumerable<PairToken> Members { get; set; } = members;

        public override string ToString()
        {
            return string.Join("", "{", string.Join(",", from member in Members select member.ToString()), "}");
        }
    }
}
