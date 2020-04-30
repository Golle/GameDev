namespace Titan.Core.EventSystem
{
    public interface IEvent
    {
        EventType Type { get; }
    }
}