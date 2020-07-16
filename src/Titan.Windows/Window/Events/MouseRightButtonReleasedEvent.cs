using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseRightButtonReleasedEvent : IEvent
    {
        public EventType Type => EventType.MouseRightButtonReleased;
    }
}
