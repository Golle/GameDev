using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseRightButtonPressedEvent : IEvent
    {
        public EventType Type => EventType.MouseRightButtonPressed;
        public int X { get; }
        public int Y { get; }

        public MouseRightButtonPressedEvent((int x, int y) position)
        {
            (X, Y) = position;
        }
    }
}
