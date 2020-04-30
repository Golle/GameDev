using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseRightButtonReleasedEvent : IEvent
    {
        public EventType Type => EventType.MouseRightButtonReleased;
        public int X { get; }
        public int Y { get; }

        public MouseRightButtonReleasedEvent((int x, int y) position)
        {
            (X, Y) = position;
        }
    }
}
