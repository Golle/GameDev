using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentDestroyedEvent : IEvent
    {
        public IComponent Component { get; }
        public EventType Type => EventType.ComponentDestroyed;
        public ComponentDestroyedEvent(IComponent component)
        {
            Component = component;
        }
    }
}