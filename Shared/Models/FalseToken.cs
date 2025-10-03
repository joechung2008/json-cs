using System;

namespace Shared.Models
{
    public class FalseToken(int skip) : Token(skip)
    {
        public override string ToString()
        {
            return bool.FalseString.ToLower();
        }
    }
}
