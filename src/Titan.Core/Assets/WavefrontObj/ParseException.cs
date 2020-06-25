using System;

namespace Titan.Core.Assets.WavefrontObj
{
    internal class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}
