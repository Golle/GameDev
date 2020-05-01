using Titan.Core.EventSystem;

namespace Titan.Core.GameLoop.Events
{
    public readonly struct UpdateEvent : IEvent
    {
        public long Ticks { get; }
        public float ElapsedTime { get; }
        public EventType Type => EventType.Update;
        public UpdateEvent(float elapsedTime, long ticks)
        {
            ElapsedTime = elapsedTime;
            Ticks = ticks;
        }
    }
}
