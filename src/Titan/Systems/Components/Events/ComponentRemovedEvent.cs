using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentRemovedEvent : IComponentEvent
    {
        public EventType Type => EventType.ComponentRemoved;
        public IComponent Component { get; }
        public ComponentRemovedEvent(IComponent component)
        {
            Component = component;
        }
    }
}
