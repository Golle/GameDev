using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentAddedEvent : IComponentEvent
    {
        public EventType Type => EventType.ComponentAdded;
        public IComponent Component { get; }
        public ComponentAddedEvent(IComponent component)
        {
            Component = component;
        }
    }
}
