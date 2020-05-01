namespace Titan.Core.Logging
{
    public interface ILogger
    {
        void Debug(string message, params object?[] arguments);
        void Info(string message, params object?[] arguments);
    }
}
