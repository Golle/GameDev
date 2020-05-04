using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct MouseMovedEvent : IEvent
    {
        public EventType Type => EventType.MouseMoved;
        public int X { get; }
        public int Y { get; }

        public MouseMovedEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
        public MouseMovedEvent((int x, int y) position)
        {
           (X, Y) = position;
        }
    }
}
