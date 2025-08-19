using Shared.Models;
using Shared.Parsers;

namespace Shared;

public static class JSON
{
    public static Token Parse(string json) => Value.Parse(json);
}
