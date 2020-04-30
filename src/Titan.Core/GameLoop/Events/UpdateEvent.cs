using Titan.Core.EventSystem;

namespace Titan.Core.GameLoop.Events
{
    public readonly struct UpdateEvent : IEvent
    {
        public long ElapsedTime { get; }
        public EventType Type => EventType.Update;
        public UpdateEvent(long elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }
    }
}
