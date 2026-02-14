
using System;

namespace Exceptions
{
    public class CustomScenarioException : BaseCustomException
    {
        public CustomScenarioException(string message) : base(message) { }
    }
}
