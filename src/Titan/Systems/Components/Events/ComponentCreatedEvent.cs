using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentCreatedEvent : IEvent
    {
        public EventType Type => EventType.ComponentCreated;
        public IComponent Component { get; }
        public ComponentCreatedEvent(IComponent component)
        {
            Component = component;
        }
    }
}