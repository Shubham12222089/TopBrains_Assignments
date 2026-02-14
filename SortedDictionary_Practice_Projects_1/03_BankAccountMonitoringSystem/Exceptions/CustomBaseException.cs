using System;

namespace Exceptions
{
    public class CustomBaseException : Exception
    {
        public CustomBaseException(string message) : base(message) { }
    }
}
