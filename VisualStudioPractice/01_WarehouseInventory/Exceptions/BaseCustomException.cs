
using System;

namespace Exceptions
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string message) : base(message) { }
    }
}
