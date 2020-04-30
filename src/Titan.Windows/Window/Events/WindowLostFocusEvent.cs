using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct WindowLostFocusEvent : IEvent
    {
        public EventType Type => EventType.LostFocus;
    }
}
