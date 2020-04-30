using Titan.Core.EventSystem;

namespace Titan.Windows.Window.Events
{
    internal readonly struct CharacterTypedEvent : IEvent
    {
        public char Character { get; }
        public EventType Type => EventType.CharacterTyped;
        public CharacterTypedEvent(char character)
        {
            Character = character;
        }
    }
}