using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentEnabledEvent : IComponentEvent
    {
        public EventType Type => EventType.ComponentEnabled;
        public IComponent Component { get; }
        public ComponentId Id => Component.Id;
        public ComponentEnabledEvent(IComponent component)
        {
            Component = component;
        }
    }
}
