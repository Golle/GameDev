using Titan.Core.Common;

namespace Titan.Core.Logging
{
    public class LogFormatter : ILogFormatter
    {
        private readonly IDateTime _dateTime;

        public LogFormatter(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public string Format(string tag, string message, object[] arguments)
        {
            return $"{_dateTime.Now.ToShortTimeString()} [{tag}] {string.Format(message, arguments)}";
        }
    }
}
