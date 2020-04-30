using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseLeftButtonReleasedEvent : IEvent
    {
        public EventType Type => EventType.MouseLeftButtonReleased;
        public int X { get; }
        public int Y { get; }

        public MouseLeftButtonReleasedEvent((int x, int y) position)
        {
            (X, Y) = position;
        }
    }
}
