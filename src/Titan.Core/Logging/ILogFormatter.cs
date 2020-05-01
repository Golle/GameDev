namespace Titan.Core.Logging
{
    internal interface ILogFormatter
    {
        string Format(string tag, string message, object?[] arguments);
    }
}
