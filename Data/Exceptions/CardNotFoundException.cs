using System;

namespace MTGCardApi.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException(string message) : base(message)
        {
        }
    }
}
