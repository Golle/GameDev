using Titan.Core.EventSystem;

namespace Titan.Systems.Components.Events
{
    internal interface IComponentEvent : IEvent
    {
        ComponentId Id { get; }
        IComponent Component { get; }
    }
}
