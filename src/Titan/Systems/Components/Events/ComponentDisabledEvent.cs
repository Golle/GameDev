using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal readonly struct ComponentDisabledEvent : IComponentEvent
    {
        public EventType Type => EventType.ComponentDisabled;
        public IComponent Component { get; }
        public ComponentDisabledEvent(IComponent component)
        {
            Component = component;
        }
    }
}
