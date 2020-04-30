using Titan.Core.EventSystem;
using Titan.Windows.Input;

namespace Titan.Windows.Window.Events
{
    public readonly struct KeyAutoRepeatEvent : IEvent
    {
        public KeyCode Key { get; }
        public EventType Type => EventType.KeyAutoRepeat;
        public KeyAutoRepeatEvent(KeyCode keyKey)
        {
            Key = keyKey;
        }
    }
}
