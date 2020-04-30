using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseLeftButtonPressedEvent : IEvent
    {
        public EventType Type => EventType.MouseLeftButtonPressed;
        public int X { get; }
        public int Y { get; }

        public MouseLeftButtonPressedEvent((int x, int y) position)
        {
            (X, Y) = position;
        }
    }
}
