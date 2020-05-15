using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    public readonly struct WindowResizeEvent : IEvent
    {
        public int Width { get; }
        public int Height { get; }
        public EventType Type => EventType.WindowResize;

        public WindowResizeEvent(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
