using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseLeftButtonPressedEvent : IEvent
    {
        public EventType Type => EventType.MouseLeftButtonPressed;
    }
}
