using Titan.Core.EventSystem;

namespace Titan.Core.GameLoop.Events
{
    public readonly struct FixedUpdateEvent : IEvent
    {
        public uint Ticks { get; }
        public EventType Type => EventType.FixedUpdate;

        public FixedUpdateEvent(uint ticks)
        {
            Ticks = ticks;
        }
    }
}