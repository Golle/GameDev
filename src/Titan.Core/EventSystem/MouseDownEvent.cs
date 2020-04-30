namespace Titan.Core.EventSystem
{
    public readonly struct MouseDownEvent : IEvent
    {
        public EventType Type => EventType.MouseDown;
    }
}
