using Titan.Core.EventSystem;
using Titan.Windows.Input;

namespace Titan.Windows.Window.Events
{
    public readonly struct KeyReleasedEvent : IEvent
    {
        public KeyCode Key { get; }
        public EventType Type => EventType.KeyReleased;

        public KeyReleasedEvent(KeyCode keyKey)
        {
            Key = keyKey;
        }
    }
}
