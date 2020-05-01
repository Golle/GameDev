using System;

namespace Titan.Core.Ioc.Exceptions
{
    [Serializable]
    internal class ToManyConstructorsException : Exception
    {
        public ToManyConstructorsException(string message) : base(message)
        {
        }
    }
}