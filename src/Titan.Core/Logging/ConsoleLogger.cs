using System;
using Titan.Core.Configuration;

namespace Titan.Core.Logging
{
    internal class ConsoleLogger : ILogger
    {
        private readonly ILogFormatter _logFormatter;
        private readonly bool _debug;

        public ConsoleLogger(IConfiguration configuration, ILogFormatter logFormatter)
        {
            _logFormatter = logFormatter;
            _debug = configuration.Debug;
        }
        
        public void Debug(string message, params object[] arguments)
        {
            if (_debug)
            {
                Console.WriteLine(_logFormatter.Format("DEBUG", message, arguments));
            }
        }

        public void Info(string message, params object[] arguments)
        {
            Console.WriteLine(_logFormatter.Format("INFO", message, arguments));
        }

        public void Error(string message, params object[] arguments)
        {
            Console.WriteLine(_logFormatter.Format("ERROR", message, arguments));
        }
    }
}
