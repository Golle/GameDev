using System;

namespace Titan.Tools.AssetsBuilder.Logging
{
    internal class Logger : ILogger
    {
        public void WriteLine(in string line)
        {
            Console.WriteLine(line);
        }
    }
}