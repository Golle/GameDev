using Titan.Core.EventSystem;
using Titan.Windows.Input;

namespace Titan.Windows.Window.Events
{
    public readonly struct KeyPressedEvent : IEvent
    {
        public KeyCode Key { get; }
        public EventType Type => EventType.KeyDown;

        public KeyPressedEvent(KeyCode keyKey)
        {
            Key = keyKey;
        }
    }
}
