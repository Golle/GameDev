using Titan.Core.EventSystem;

namespace Titan.Core.GameLoop.Events
{
    public readonly struct FixedUpdateEvent : IEvent
    {
        public float ElapsedTime { get; }
        public EventType Type => EventType.FixedUpdate;

        public FixedUpdateEvent(float elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }
    }
}
