using System;

namespace Titan.Core.Ioc.Exceptions
{
    [Serializable]
    internal class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message) : base(message)
        {
        }
    }
}