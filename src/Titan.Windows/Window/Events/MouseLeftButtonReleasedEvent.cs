using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseLeftButtonReleasedEvent : IEvent
    {
        public EventType Type => EventType.MouseLeftButtonReleased;
    }
}
