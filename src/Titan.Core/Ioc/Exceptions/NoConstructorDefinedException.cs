using System;

namespace Titan.Core.Ioc.Exceptions
{
    [Serializable]
    internal class NoConstructorDefinedException : Exception
    {
        public NoConstructorDefinedException(string message)
            : base(message)
        {
        }
    }
}
