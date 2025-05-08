using System;

namespace MTGCardApi.Exceptions
{
    // custom exception that gets thrown when a card isn't found
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException(string message) : base(message) { }
    }
}
