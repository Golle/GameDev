using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseRightButtonPressedEvent : IEvent
    {
        public EventType Type => EventType.MouseRightButtonPressed;
    }
}
