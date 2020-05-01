using System;

namespace Titan.Core.Ioc.Exceptions
{
    [Serializable]
    internal class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException(string message) : base(message)
        {
        }
    }
}